using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Objectives;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class ObjectiveRepository : IObjectiveWriteOnlyRepository, IObjetiveReadOnlyRepository, IObjetiveUpdateOnlyRepository
{
  private readonly OkrDbContext _context;

  public ObjectiveRepository(OkrDbContext context)
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

  public async Task<List<Objective>> GetByQuarterAndYear(int quarter, int year)
  {
    return await _context.Objectives.Where(x => x.Quarter == quarter && x.Year == year).ToListAsync();
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
    Objective? result = await _context.Objectives.FirstOrDefaultAsync(predicate: objetive => objetive.Id == id);

    if (result == null)
    {
      return false;
    }

    _context.Objectives.Remove(entity: result);
    return true;
  }
}
