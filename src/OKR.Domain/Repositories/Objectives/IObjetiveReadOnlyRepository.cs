using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjetiveReadOnlyRepository
{
  Task<List<ObjectiveEntity>> GetAll();
  Task<List<ObjectiveEntity>> GetByQuarterAndYear(int quarter, int year);
}
