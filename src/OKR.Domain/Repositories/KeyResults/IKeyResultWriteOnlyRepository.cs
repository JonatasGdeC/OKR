using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.KeyResults;

public interface IKeyResultWriteOnlyRepository
{
  Task Add(KeyResult keyResult);
}
