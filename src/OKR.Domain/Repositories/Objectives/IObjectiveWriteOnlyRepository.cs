using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjectiveWriteOnlyRepository
{
  Task Add(Objective objective);
  /// <summary>
  /// This function returns TRUE if the deletion was successful otherwise returns false
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<bool> Delete(Guid id);
}
