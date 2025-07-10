using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Actions;

public interface IActionUpdateOnlyRepository
{
  Task<ActionEntity?> GetById(Entities.User loggedUser, Guid actionId);
  Task Update(ActionEntity action);
  Task UpdateProgress(ActionEntity action, int progress);
}
