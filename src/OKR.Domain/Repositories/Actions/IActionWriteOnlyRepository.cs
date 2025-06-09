using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Actions;

public interface IActionWriteOnlyRepository
{
  Task Add(ActionEntity action);
  /// <summary>
  /// This function returns TRUE if the deletion was successful otherwise returns false
  /// </summary>
  Task<bool> Delete(Guid id);
}

