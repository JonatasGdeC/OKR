using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.Delete;

public class DeleteFeedbackUseCase : IDeleteFeedbackUseCase
{
  private readonly IFeedbackWriteOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteFeedbackUseCase(IFeedbackWriteOnlyRepository repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(Guid feedbackId)
  {
    bool result = await _repository.DeleteFeedback(feedbackId: feedbackId);

    if (!result)
    {
      throw new NotFoundException("Feedback not found");
    }

    await _unitOfWork.Commit();
  }
}
