using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Actions;

public interface IActionReadOnlyRepository
{
  Task<List<ActionEntity>?> GetActionsByKeyResultId(Entities.User loggedUser, Guid keyResultId);
  Task<List<ActionEntity>?> GetActionsByDateRange(Entities.User loggedUser, DateTime dateStart, DateTime dateEnd);
  Task<ActionEntity?> GetActionById(Entities.User loggedUser, Guid actionId);
}
