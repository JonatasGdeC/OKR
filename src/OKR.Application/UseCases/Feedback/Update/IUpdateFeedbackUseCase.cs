using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Feedback.Update;

public interface IUpdateFeedbackUseCase
{
  Task Execute(Guid feedbackId, RequestRegisterFeedbackJson request);
}
