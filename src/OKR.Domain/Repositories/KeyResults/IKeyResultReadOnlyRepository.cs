using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.KeyResults;

public interface IKeyResultReadOnlyRepository
{
  Task<List<KeyResultEntity>?> GetKeyResultsByObjectiveId(Entities.User loggedUser, Guid id);
  Task<KeyResultEntity?> GetById(Entities.User loggedUser, Guid id);
}
