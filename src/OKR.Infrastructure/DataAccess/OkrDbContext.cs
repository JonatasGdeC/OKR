using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;

namespace OKR.Infrastructure.DataAccess;

internal class OkrDbContext : DbContext
{
  public OkrDbContext(DbContextOptions options) : base(options) { }
  public DbSet<Objective> Objectives { get; set; }
}
