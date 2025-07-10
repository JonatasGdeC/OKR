using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Feedbacks;

public interface IFeedbackReadOnlyRepository
{
  Task<List<FeedbackEntity>?> GetFeedbacksByActionId(Entities.User loggedUser, Guid actionId);
  Task<FeedbackEntity?> GetFeedbackById(Entities.User loggedUser, Guid feedbackId);
  Task<List<FeedbackEntity>?> GetFeedbacksByDateRange(Entities.User loggedUser, DateTime dateStart, DateTime dateEnd);
}
