using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjetiveUpdateOnlyRepository
{
  Task<ObjectiveEntity?> GetById(Guid id);
  Task Update(ObjectiveEntity objective);
}
