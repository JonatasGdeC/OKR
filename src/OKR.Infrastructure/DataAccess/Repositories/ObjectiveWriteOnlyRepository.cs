using OKR.Domain.Entities;
using OKR.Domain.Repositories.Objectives;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class ObjectiveWriteOnlyRepository : IObjectiveWriteOnlyRepository
{
  private readonly OkrDbContext _context;

  public ObjectiveWriteOnlyRepository(OkrDbContext context)
  {
    _context = context;
  }

  public async Task Add(Objective objective)
  {
    await _context.Objectives.AddAsync(objective);
  }
}
