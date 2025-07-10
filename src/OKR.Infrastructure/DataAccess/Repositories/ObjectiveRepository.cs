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

  public async Task Add(ObjectiveEntity objective)
  {
    await _context.Objectives.AddAsync(entity: objective);
  }

  public async Task<List<ObjectiveEntity>> GetAll(User loggedUser)
  {
    return await _context.Objectives.AsNoTracking().Where(predicate: objective => objective.UserId == loggedUser.Id).ToListAsync();
  }

  public async Task<List<ObjectiveEntity>> GetByQuarterAndYear(User loggedUser, int quarter, int year)
  {
    return await _context.Objectives.AsNoTracking().Where(predicate: objective => objective.Quarter == quarter && objective.Year == year && objective.UserId == loggedUser.Id).ToListAsync();
  }

  async Task<ObjectiveEntity?> IObjetiveReadOnlyRepository.GetById(User loggedUser, Guid id)
  {
    return await _context.Objectives.AsNoTracking().FirstOrDefaultAsync(predicate: objective => objective.Id == id && objective.UserId == loggedUser.Id);
  }

  async Task<ObjectiveEntity?> IObjetiveUpdateOnlyRepository.GetById(User loggedUser, Guid id)
  {
    return await _context.Objectives.AsNoTracking().FirstOrDefaultAsync(predicate: objective => objective.Id == id && objective.UserId == loggedUser.Id);
  }

  public async Task Update(ObjectiveEntity objective)
  {
    _context.Objectives.Update(entity: objective);
  }

  public async Task Delete(Guid id)
  {
    ObjectiveEntity? result = await _context.Objectives.FirstOrDefaultAsync(predicate: objetive => objetive.Id == id);
    _context.Objectives.Remove(entity: result!);
  }
}
