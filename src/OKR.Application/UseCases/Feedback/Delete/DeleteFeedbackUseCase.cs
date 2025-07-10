using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.Delete;

public class DeleteFeedbackUseCase : IDeleteFeedbackUseCase
{
  private readonly IFeedbackWriteOnlyRepository _feedbackWriteOnlyRepository;
  private readonly IFeedbackReadOnlyRepository _feedbackReadOnlyRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILoggedUser _loggedUser;

  public DeleteFeedbackUseCase(
    IFeedbackWriteOnlyRepository feedbackWriteOnlyRepository,
    IFeedbackReadOnlyRepository feedbackReadOnlyRepository,
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser)
  {
    _feedbackWriteOnlyRepository = feedbackWriteOnlyRepository;
    _feedbackReadOnlyRepository = feedbackReadOnlyRepository;
    _unitOfWork = unitOfWork;
    _loggedUser = loggedUser;
  }

  public async Task Execute(Guid feedbackId)
  {
    var loggedUser = await _loggedUser.Get();

    FeedbackEntity? feedback = await _feedbackReadOnlyRepository.GetFeedbackById(loggedUser: loggedUser, feedbackId: feedbackId);

    if (feedback == null)
    {
      throw new NotFoundException(message: "Feedback not found");
    }

    await _feedbackWriteOnlyRepository.DeleteFeedback(feedback: feedback);
    await _unitOfWork.Commit();
  }
}
