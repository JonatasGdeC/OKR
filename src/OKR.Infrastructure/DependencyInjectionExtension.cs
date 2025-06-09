using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Repositories.KeyResults;
using OKR.Domain.Repositories.Objectives;
using OKR.Infrastructure.DataAccess;
using OKR.Infrastructure.DataAccess.Repositories;

namespace OKR.Infrastructure;

public static class DependencyInjectionExtension
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    AddDbContext(services: services, configuration: configuration);
    AddRepositories(services: services);
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

    services.AddScoped<IUnitOfWork, UnitOfWork>();
  }
}
