using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;

namespace OKR.Infrastructure.DataAccess;

internal class OkrDbContext : DbContext
{
  public OkrDbContext(DbContextOptions options) : base(options: options) { }
  public DbSet<ObjectiveEntity> Objectives { get; set; }
  public DbSet<KeyResultEntity> KeyResults { get; set; }
  public DbSet<ActionEntity> Actions { get; set; }
  public DbSet<FeedbackEntity> Feedbacks { get; set; }
  public DbSet<User> Users  { get; set; }
}
