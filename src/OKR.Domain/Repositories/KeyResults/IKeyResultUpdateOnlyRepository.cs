using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.KeyResults;

public interface IKeyResultUpdateOnlyRepository
{
  Task<KeyResultEntity?> GetById(Entities.User loggedUser, Guid id);
  Task Update(KeyResultEntity keyResult);
}
