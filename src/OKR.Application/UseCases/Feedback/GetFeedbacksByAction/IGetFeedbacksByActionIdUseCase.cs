using OKR.Communication.Response;

namespace OKR.Application.UseCases.Feedback.GetFeedbacksByAction;

public interface IGetFeedbacksByActionIdUseCase
{
  Task<ResponseListFeedbacksJson> Execute(Guid actionId);
}
