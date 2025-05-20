using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.KeyResults;

public interface IKeyResultReadOnlyRepository
{
  Task<List<KeyResult>?> GetKeyResultsByObjectiveId(Guid id);
}
