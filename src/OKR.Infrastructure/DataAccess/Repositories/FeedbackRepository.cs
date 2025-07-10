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

  public async Task<List<FeedbackEntity>?> GetFeedbacksByActionId(User loggedUser, Guid actionId)
  {
    return await _context.Feedbacks.Where(predicate: feedback => feedback.ActionId == actionId && feedback.UserId == loggedUser.Id).ToListAsync();
  }

  async Task<FeedbackEntity?> IFeedbackReadOnlyRepository.GetFeedbackById(User loggedUser, Guid feedbackId)
  {
    return await _context.Feedbacks.AsNoTracking().FirstOrDefaultAsync(predicate: feedback => feedback.Id == feedbackId && feedback.UserId == loggedUser.Id);
  }

  public async Task<List<FeedbackEntity>?> GetFeedbacksByDateRange(User loggedUser, DateTime dateStart, DateTime dateEnd)
  {
    return await _context.Feedbacks.Where(predicate: feedback => feedback.Date.Date >= dateStart.Date && feedback.Date.Date <= dateEnd.Date && feedback.UserId == loggedUser.Id).ToListAsync();
  }

  async Task<FeedbackEntity?> IFeedbackUpdateOnlyRepository.GetFeedbackById(User loggedUser, Guid feedbackId)
  {
    return await _context.Feedbacks.FirstOrDefaultAsync(predicate: feedback => feedback.Id == feedbackId && feedback.UserId == loggedUser.Id);
  }

  public async Task Update(FeedbackEntity feedback)
  {
    _context.Feedbacks.Update(entity: feedback);
  }

  public async Task AddFeedback(FeedbackEntity feedback)
  {
    await _context.Feedbacks.AddAsync(entity: feedback);
  }

  public async Task DeleteFeedback(FeedbackEntity feedback)
  {
    _context.Feedbacks.Remove(entity: feedback);
  }
}
