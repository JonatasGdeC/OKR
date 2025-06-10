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

  public async Task<ActionEntity?> GetById(Guid actionId)
  {
    return await _context.Actions.FirstOrDefaultAsync(x => x.Id == actionId);
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
