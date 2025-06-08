using OKR.Communication.Response;

namespace OKR.Application.UseCases.Objetives.GetByQuarterAndYear;

public interface IGetObjectiveByQuarterAndYear
{
  Task <ResponseListObjectiveJson> Execute(int quarter, int year);
}
