using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OKR.API.Filters;
using OKR.API.Middleware;
using OKR.API.Token;
using OKR.Application;
using OKR.Domain.Secury.Tokens;
using OKR.Infrastructure;

var builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowBlazorClient", policy =>
  {
    policy
      .WithOrigins("http://localhost:5058")
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});

builder.Services.AddSwaggerGen(setupAction: config =>
{
  config.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
    In = ParameterLocation.Header,
    Scheme = "Bearer",
    Type = SecuritySchemeType.ApiKey,
  });

  config.AddSecurityRequirement(securityRequirement: new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,
      },
      new List<string>()
    }
  });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddRouting(configureOptions: option => option.LowercaseUrls = true);

builder.Services.AddMvc(setupAction: options => options.Filters.Add(filterType: typeof(ExceptionFilter)));

builder.Services.AddInfrastructure(configuration: builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(configureOptions: config =>
{
  config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions: config =>
{
  config.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = false,
    ValidateAudience = false,
    ClockSkew = new TimeSpan(ticks: 0),
    IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: builder.Configuration.GetValue<string>(key: "Settings:JWT:SigningKey")!))
  };
});

var app = builder.Build();

app.UseCors("AllowBlazorClient");

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await MigrateDatabase();

app.Run();

async Task MigrateDatabase()
{
  await using var scope = app.Services.CreateAsyncScope();
  await DataBaseMigration.MigrateDatabase(serviceProvider: scope.ServiceProvider);
}
