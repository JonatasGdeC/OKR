using OKR.Communication.Response;

namespace OKR.Application.UseCases.Action.GetActionsByDateRange;

public interface IGetActionsByDateRangeUseCase
{
  Task<ResponseListActionJson> Execute(DateTime startDate, DateTime endDate);
}
