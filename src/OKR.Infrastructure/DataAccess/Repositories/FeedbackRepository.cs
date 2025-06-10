using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Feedbacks;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class FeedbackRepository : IFeedbackReadOnlyRepository, IFeedbackUpdateOnlyRepository, IFeedbackWriteOnlyRepository
{
  private readonly OkrDbContext _context;

  public FeedbackRepository(OkrDbContext context)
  {
    _context = context;
  }

  public async Task<List<FeedbackEntity>?> GetFeedbacksByActionId(Guid actionId)
  {
    return await _context.Feedbacks.Where(feedback => feedback.ActionId == actionId).ToListAsync();
  }

  public async Task<List<FeedbackEntity>?> GetFeedbacksByDateRange(DateTime dateStart, DateTime dateEnd)
  {
    return await _context.Feedbacks.Where(feedback => feedback.Date.Date >= dateStart.Date && feedback.Date.Date <= dateEnd.Date).ToListAsync();
  }

  async Task<FeedbackEntity?> IFeedbackUpdateOnlyRepository.GetFeedbackById(Guid feedbackId)
  {
    return await _context.Feedbacks.FirstOrDefaultAsync(feedback => feedback.Id == feedbackId);
  }

  public async Task Update(FeedbackEntity feedback)
  {
    _context.Feedbacks.Update(feedback);
  }

  public async Task AddFeedback(FeedbackEntity feedback)
  {
    await _context.Feedbacks.AddAsync(entity: feedback);
  }

  public async Task<bool> DeleteFeedback(Guid feedbackId)
  {
    FeedbackEntity? result = await _context.Feedbacks.FirstOrDefaultAsync(predicate: feedback => feedback.Id == feedbackId);

    if (result == null)
    {
      return false;
    }

    _context.Feedbacks.Remove(entity: result);
    return true;
  }
}
