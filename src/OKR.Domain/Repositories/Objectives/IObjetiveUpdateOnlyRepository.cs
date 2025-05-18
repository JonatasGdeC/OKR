using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjetiveUpdateOnlyRepository
{
  Task<Objective?> GetById(Guid id);
  Task Update(Objective objective);
}
