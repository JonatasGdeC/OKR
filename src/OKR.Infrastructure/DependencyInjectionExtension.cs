using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Objectives;
using OKR.Infrastructure.DataAccess;
using OKR.Infrastructure.DataAccess.Repositories;

namespace OKR.Infrastructure;

public static class DependencyInjectionExtension
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    AddDbContext(services, configuration);
    AddRepositories(services);
  }

  private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("connection");
    var version = new Version(8, 0, 41);
    var serverVersion = new MySqlServerVersion(version);

    services.AddDbContext<OkrDbContext>(config => config.UseMySql(connectionString, serverVersion));
  }


  private static void AddRepositories(IServiceCollection services)
  {
    services.AddScoped<IObjectiveWriteOnlyRepository, ObjectiveWriteOnlyRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
  }
}
