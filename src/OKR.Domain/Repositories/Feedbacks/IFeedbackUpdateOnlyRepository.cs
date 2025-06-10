using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Feedbacks;

public interface IFeedbackUpdateOnlyRepository
{
  Task<FeedbackEntity?> GetFeedbackById(Guid feedbackId);
  Task Update(FeedbackEntity feedback);
}
