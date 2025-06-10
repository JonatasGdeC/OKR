using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Actions;

public interface IActionReadOnlyRepository
{
  Task<List<ActionEntity>?> GetActionsByKeyResultId(Guid keyResultId);
  Task<List<ActionEntity>?> GetActionsByDateRange(DateTime dateStart, DateTime dateEnd);
  Task<ActionEntity?> GetActionById(Guid actionId);
}
