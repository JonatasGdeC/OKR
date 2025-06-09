using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.KeyResults;

public interface IKeyResultWriteOnlyRepository
{
  Task Add(KeyResultEntity keyResult);
  /// <summary>
  /// This function returns TRUE if the deletion was successful otherwise returns false
  /// </summary>
  Task<bool> Delete(Guid id);
}
