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

  public async Task<List<ActionEntity>?> GetActionsByKeyResultId(User loggedUser, Guid keyResultId)
  {
    return await _context.Actions.AsNoTracking().Where(predicate: action => action.KeyResultId == keyResultId && action.UserId == loggedUser.Id).ToListAsync();
  }

  public async Task<List<ActionEntity>?> GetActionsByDateRange(User loggedUser, DateTime dateStart, DateTime dateEnd)
  {
    return await _context.Actions.AsNoTracking().Where(predicate: action => action.EndDate.Date >= dateStart.Date && action.StartDate.Date <= dateEnd.Date && action.UserId == loggedUser.Id).ToListAsync();
  }

  async Task<ActionEntity?> IActionReadOnlyRepository.GetActionById(User loggedUser, Guid actionId)
  {
    return await _context.Actions.FirstOrDefaultAsync(predicate: action => action.Id == actionId && action.UserId == loggedUser.Id);
  }

  async Task<ActionEntity?> IActionUpdateOnlyRepository.GetById(User loggedUser, Guid actionId)
  {
    return await _context.Actions.FirstOrDefaultAsync(predicate: action => action.Id == actionId && action.UserId == loggedUser.Id);
  }

  public async Task Update(ActionEntity action)
  {
    _context.Actions.Update(entity: action);
  }

  public async Task UpdateProgress(ActionEntity action, int progress)
  {
    action.CurrentProgress = progress;
    _context.Actions.Update(entity: action);
  }

  public async Task Add(ActionEntity action)
  {
    await _context.Actions.AddAsync(entity: action);
  }

  public async Task Delete(Guid id)
  {
    ActionEntity? result = await _context.Actions.FirstOrDefaultAsync(predicate: action => action.Id == id);
    _context.Actions.Remove(entity: result!);
  }
}
