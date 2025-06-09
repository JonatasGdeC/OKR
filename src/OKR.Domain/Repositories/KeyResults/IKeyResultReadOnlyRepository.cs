using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.KeyResults;

public interface IKeyResultReadOnlyRepository
{
  Task<List<KeyResultEntity>?> GetKeyResultsByObjectiveId(Guid id);
  Task<KeyResultEntity?> GetById(Guid id);
}
