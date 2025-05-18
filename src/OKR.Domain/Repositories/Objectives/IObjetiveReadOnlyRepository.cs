using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjetiveReadOnlyRepository
{
  Task<List<Objective>> GetAll();
}
