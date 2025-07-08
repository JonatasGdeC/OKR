using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjectiveWriteOnlyRepository
{
  Task Add(ObjectiveEntity objective);
  Task Delete(Guid id);
}
