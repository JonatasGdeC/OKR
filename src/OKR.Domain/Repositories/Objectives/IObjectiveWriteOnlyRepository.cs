using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjectiveWriteOnlyRepository
{
  Task Add(ObjectiveEntity objective);
  /// <summary>
  /// This function returns TRUE if the deletion was successful otherwise returns false
  /// </summary>
  Task<bool> Delete(Guid id);
}
