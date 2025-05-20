using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.KeyResults;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class KeyResultRepository : IKeyResultReadOnlyRepository, IKeyResultWriteOnlyRepository
{
  private readonly OkrDbContext _context;

  public KeyResultRepository(OkrDbContext context)
  {
    _context = context;
  }

  public async Task Add(KeyResult keyResult)
  {
    await _context.KeyResults.AddAsync(entity: keyResult);
  }

  public async Task<List<KeyResult>?> GetKeyResultsByObjectiveId(Guid id)
  {
    return await _context.KeyResults.Where(kr => kr.ObjectiveId == id).AsNoTracking().ToListAsync();
  }
}
