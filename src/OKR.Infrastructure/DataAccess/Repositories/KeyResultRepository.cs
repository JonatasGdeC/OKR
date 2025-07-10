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

  public async Task Delete(Guid id)
  {
    KeyResultEntity? result = await _context.KeyResults.FirstOrDefaultAsync(predicate: keyResult => keyResult.Id == id);
    _context.KeyResults.Remove(entity: result!);
  }

  public async Task<List<KeyResultEntity>?> GetKeyResultsByObjectiveId(User loggedUser, Guid id)
  {
    return await _context.KeyResults.AsNoTracking().Where(predicate: kr => kr.ObjectiveId == id && kr.UserId == loggedUser.Id).ToListAsync();
  }

  async Task<KeyResultEntity?> IKeyResultReadOnlyRepository.GetById(User loggedUser, Guid id)
  {
    return await _context.KeyResults.AsNoTracking().FirstOrDefaultAsync(predicate: kerResult => kerResult.Id == id && kerResult.UserId == loggedUser.Id);
  }

  async Task<KeyResultEntity?> IKeyResultUpdateOnlyRepository.GetById(User loggedUser, Guid id)
  {
    return await _context.KeyResults.FirstOrDefaultAsync(predicate: keyResult => keyResult.Id == id && keyResult.UserId == loggedUser.Id);
  }

  public async Task Update(KeyResultEntity keyResult)
  {
    _context.KeyResults.Update(entity: keyResult);
  }
}
