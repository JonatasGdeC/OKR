using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjetiveReadOnlyRepository
{
  Task<List<ObjectiveEntity>> GetAll(Entities.User loggedUser);
  Task<List<ObjectiveEntity>> GetByQuarterAndYear(Entities.User loggedUser, int quarter, int year);
  Task<ObjectiveEntity?> GetById(Entities.User loggedUser, Guid id);
}
