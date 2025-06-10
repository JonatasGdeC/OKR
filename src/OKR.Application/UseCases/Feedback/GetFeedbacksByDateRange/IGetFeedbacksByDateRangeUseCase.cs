using OKR.Communication.Response;

namespace OKR.Application.UseCases.Feedback.GetFeedbacksByDateRange;

public interface IGetFeedbacksByDateRangeUseCase
{
  Task<ResponseListFeedbacksJson> Execute(DateTime dateStart, DateTime dateEnd);
}
