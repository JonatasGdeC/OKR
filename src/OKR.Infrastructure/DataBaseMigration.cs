using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OKR.Infrastructure.DataAccess;

namespace OKR.Infrastructure;

public static class DataBaseMigration
{
  public static async Task MigrateDatabase(IServiceProvider serviceProvider)
  {
    var dbContext = serviceProvider.GetRequiredService<OkrDbContext>();
    await dbContext.Database.MigrateAsync();
  }
}
