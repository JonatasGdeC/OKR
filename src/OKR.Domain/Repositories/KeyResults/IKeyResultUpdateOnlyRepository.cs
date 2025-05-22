using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.KeyResults;

public interface IKeyResultUpdateOnlyRepository
{
  Task<KeyResult?> GetById(Guid id);
  Task Update(KeyResult keyResult);
}
