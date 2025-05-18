using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Objectives;

public interface IObjectiveWriteOnlyRepository
{
  Task Add(Objective objective);
}
