using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Actions;

public interface IActionReadOnlyRepository
{
  Task<List<ActionEntity>?> GetActionsByKeyResultId(Guid keyResultId);
  Task<ActionEntity?> GetActionById(Guid actionId);
}
