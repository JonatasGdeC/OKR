using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Objectives;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class ObjectiveWriteOnlyRepository : IObjectiveWriteOnlyRepository, IObjetiveReadOnlyRepository, IObjetiveUpdateOnlyRepository
{
  private readonly OkrDbContext _context;

  public ObjectiveWriteOnlyRepository(OkrDbContext context)
  {
    _context = context;
  }

  public async Task Add(Objective objective)
  {
    await _context.Objectives.AddAsync(entity: objective);
  }

  public async Task<List<Objective>> GetAll()
  {
    return await _context.Objectives.AsNoTracking().ToListAsync();
  }

  async Task<Objective?> IObjetiveUpdateOnlyRepository.GetById(Guid id)
  {
    return await _context.Objectives.FirstOrDefaultAsync(predicate: objective => objective.Id == id);
  }

  public async Task Update(Objective objective)
  {
    _context.Objectives.Update(entity: objective);
  }

  public async Task<bool> Delete(Guid id)
  {
    Objective? result = await _context.Objectives.FirstOrDefaultAsync(predicate: expense => expense.Id == id);

    if (result == null)
    {
      return false;
    }

    _context.Objectives.Remove(entity: result);
    return true;
  }
}
