using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.KeyResults;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class KeyResultRepository : IKeyResultReadOnlyRepository, IKeyResultWriteOnlyRepository, IKeyResultUpdateOnlyRepository
{
  private readonly OkrDbContext _context;

  public KeyResultRepository(OkrDbContext context)
  {
    _context = context;
  }

  public async Task Add(KeyResultEntity keyResult)
  {
    await _context.KeyResults.AddAsync(entity: keyResult);
  }

  public async Task<bool> Delete(Guid id)
  {
    KeyResultEntity? result = await _context.KeyResults.FirstOrDefaultAsync(predicate: keyResult => keyResult.Id == id);

    if (result == null)
    {
      return false;
    }

    _context.KeyResults.Remove(entity: result);
    return true;
  }

  public async Task<List<KeyResultEntity>?> GetKeyResultsByObjectiveId(Guid id)
  {
    return await _context.KeyResults.Where(predicate: kr => kr.ObjectiveId == id).AsNoTracking().ToListAsync();
  }

  public async Task<KeyResultEntity?> GetById(Guid id)
  {
    return await _context.KeyResults.FirstOrDefaultAsync(predicate: keyResult => keyResult.Id == id);
  }

  public async Task Update(KeyResultEntity keyResult)
  {
    _context.KeyResults.Update(entity: keyResult);
  }
}
