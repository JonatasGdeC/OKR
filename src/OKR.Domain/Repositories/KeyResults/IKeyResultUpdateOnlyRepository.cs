using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.KeyResults;

public interface IKeyResultUpdateOnlyRepository
{
  Task<KeyResultEntity?> GetById(Guid id);
  Task Update(KeyResultEntity keyResult);
}
