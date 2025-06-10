namespace OKR.Application.UseCases.Feedback.Delete;

public interface IDeleteFeedbackUseCase
{
  Task Execute(Guid feedbackId);
}
