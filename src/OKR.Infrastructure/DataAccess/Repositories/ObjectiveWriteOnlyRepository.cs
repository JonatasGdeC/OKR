using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Objectives;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class ObjectiveWriteOnlyRepository : IObjectiveWriteOnlyRepository, IObjetiveReadOnlyRepository
{
  private readonly OkrDbContext _context;

  public ObjectiveWriteOnlyRepository(OkrDbContext context)
  {
    _context = context;
  }

  public async Task Add(Objective objective)
  {
    await _context.Objectives.AddAsync(objective);
  }

  public async Task<List<Objective>> GetAll()
  {
    return await _context.Objectives.AsNoTracking().ToListAsync();
  }
}
