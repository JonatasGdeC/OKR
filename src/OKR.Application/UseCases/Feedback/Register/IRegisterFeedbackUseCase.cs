using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.Application.UseCases.Feedback.Register;

public interface IRegisterFeedbackUseCase
{
  Task<ResponseFeedbackJson> Execute(RequestRegisterFeedbackJson request);
}
