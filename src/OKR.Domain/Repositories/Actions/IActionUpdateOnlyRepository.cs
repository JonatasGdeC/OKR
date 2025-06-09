using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Actions;

public interface IActionUpdateOnlyRepository
{
  Task<ActionEntity?> GetById(Guid actionId);
  Task Update(ActionEntity action);
}
