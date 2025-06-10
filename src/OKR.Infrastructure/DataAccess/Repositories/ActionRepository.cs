using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Actions;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class ActionRepository : IActionReadOnlyRepository, IActionUpdateOnlyRepository, IActionWriteOnlyRepository
{
  private readonly OkrDbContext _context;

  public ActionRepository(OkrDbContext context)
  {
    _context = context;
  }

  public async Task<List<ActionEntity>?> GetActionsByKeyResultId(Guid KeyResultId)
  {
    return await _context.Actions.Where(x => x.KeyResultId == KeyResultId).ToListAsync();
  }

  public async Task<List<ActionEntity>?> GetActionsByDateRange(DateTime dateStart, DateTime dateEnd)
  {
    return await _context.Actions.Where(action => action.EndDate >= dateStart && action.StartDate <= dateEnd).ToListAsync();
  }

  async Task<ActionEntity?> IActionReadOnlyRepository.GetActionById(Guid actionId)
  {
    return await _context.Actions.FirstOrDefaultAsync(action => action.Id == actionId);
  }

  public async Task<ActionEntity?> GetById(Guid actionId)
  {
    return await _context.Actions.FirstOrDefaultAsync(action => action.Id == actionId);
  }

  public async Task Update(ActionEntity action)
  {
    _context.Actions.Update(action);
  }

  public async Task UpdateProgress(ActionEntity action, int progress)
  {
    action.CurrentProgress = progress;
    _context.Actions.Update(action);
  }

  public async Task Add(ActionEntity action)
  {
    await _context.Actions.AddAsync(action);
  }

  public async Task<bool> Delete(Guid id)
  {
    ActionEntity? result = await _context.Actions.FirstOrDefaultAsync(predicate: action => action.Id == id);

    if (result == null)
    {
      return false;
    }

    _context.Actions.Remove(entity: result);
    return true;
  }
}
