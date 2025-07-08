using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjetiveUpdateOnlyRepository
{
  Task<ObjectiveEntity?> GetById(Entities.User loggedUser, Guid id);
  Task Update(ObjectiveEntity objective);
}
