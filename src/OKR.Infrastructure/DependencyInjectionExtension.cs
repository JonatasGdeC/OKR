using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Domain.Repositories.KeyResults;
using OKR.Domain.Repositories.Objectives;
using OKR.Domain.Repositories.User;
using OKR.Domain.Secury;
using OKR.Domain.Secury.Cryptography;
using OKR.Domain.Services.LoggedUser;
using OKR.Domain.Tokens;
using OKR.Infrastructure.DataAccess;
using OKR.Infrastructure.DataAccess.Repositories;
using OKR.Infrastructure.Secury.Token;
using OKR.Infrastructure.Services.LoggedUser;

namespace OKR.Infrastructure;

public static class DependencyInjectionExtension
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    AddDbContext(services: services, configuration: configuration);
    AddRepositories(services: services);
    AddToken(services: services, configuration: configuration);

    services.AddScoped<IPasswordEncripter, Secury.Cryptography.BCrypt>();
    services.AddScoped<ILoggedUser, LoggedUser>();
  }

  private static void AddToken(IServiceCollection services, IConfiguration configuration)
  {
    var expiration = configuration.GetValue<uint>("Settings:JWT:ExpiresMinutes");
    var signingKey = configuration.GetValue<string>("Settings:JWT:SigningKey");

    services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expiration, signingKey!));
  }

  private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString(name: "connection");
    var version = new Version(major: 8, minor: 0, build: 41);
    var serverVersion = new MySqlServerVersion(version: version);

    services.AddDbContext<OkrDbContext>(optionsAction: config => config.UseMySql(connectionString: connectionString, serverVersion: serverVersion));
  }


  private static void AddRepositories(IServiceCollection services)
  {
    services.AddScoped<IObjectiveWriteOnlyRepository, ObjectiveRepository>();
    services.AddScoped<IObjetiveReadOnlyRepository, ObjectiveRepository>();
    services.AddScoped<IObjetiveUpdateOnlyRepository, ObjectiveRepository>();

    services.AddScoped<IKeyResultReadOnlyRepository, KeyResultRepository>();
    services.AddScoped<IKeyResultWriteOnlyRepository, KeyResultRepository>();
    services.AddScoped<IKeyResultUpdateOnlyRepository, KeyResultRepository>();

    services.AddScoped<IActionReadOnlyRepository, ActionRepository>();
    services.AddScoped<IActionUpdateOnlyRepository, ActionRepository>();
    services.AddScoped<IActionWriteOnlyRepository, ActionRepository>();

    services.AddScoped<IFeedbackReadOnlyRepository, FeedbackRepository>();
    services.AddScoped<IFeedbackUpdateOnlyRepository, FeedbackRepository>();
    services.AddScoped<IFeedbackWriteOnlyRepository, FeedbackRepository>();

    services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

    services.AddScoped<IUnitOfWork, UnitOfWork>();
  }
}
