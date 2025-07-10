using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Feedbacks;

public interface IFeedbackWriteOnlyRepository
{
  Task AddFeedback(FeedbackEntity feedback);
  Task DeleteFeedback(FeedbackEntity feedback);
}
