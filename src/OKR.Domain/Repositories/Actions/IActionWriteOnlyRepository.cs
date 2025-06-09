using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Actions;

public interface IActionWriteOnlyRepository
{
  Task Add(ActionEntity action);
}

