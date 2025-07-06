using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using OKR.API.Filters;
using OKR.API.Middleware;
using OKR.Application;
using OKR.Infrastructure;

var builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddRouting(configureOptions: option => option.LowercaseUrls = true);

builder.Services.AddMvc(setupAction: options => options.Filters.Add(filterType: typeof(ExceptionFilter)));

builder.Services.AddInfrastructure(configuration: builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddAuthentication(config =>
{
  config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
  config.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = false,
    ValidateAudience = false,
    ClockSkew = new TimeSpan(0),
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Settings:JWT:SigningKey")!))
  };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

await MigrateDatabase();

app.Run();

async Task MigrateDatabase()
{
  await using var scope = app.Services.CreateAsyncScope();
  await DataBaseMigration.MigrateDatabase(scope.ServiceProvider);
}
