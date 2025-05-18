using OKR.Domain.Repositories;

namespace OKR.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
  private readonly OkrDbContext _context;

  public UnitOfWork(OkrDbContext context)
  {
    _context = context;
  }

  public async Task Commit() => await _context.SaveChangesAsync();
}
