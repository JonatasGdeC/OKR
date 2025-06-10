using OKR.Domain.Entities;

namespace OKR.Domain.Repositories.Feedbacks;

public interface IFeedbackWriteOnlyRepository
{
  Task AddFeedback(FeedbackEntity feedback);
  /// <summary>
  /// This function returns TRUE if the deletion was successful otherwise returns false
  /// </summary>
  Task<bool> DeleteFeedback(Guid feedbackId);
}
