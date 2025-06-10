using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Feedbacks;

public interface IFeedbackReadOnlyRepository
{
  Task<List<FeedbackEntity>?> GetFeedbacksByActionId(Guid actionId);
  Task<List<FeedbackEntity>?> GetFeedbacksByDateRange(DateTime dateStart, DateTime dateEnd);
}
