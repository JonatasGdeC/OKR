using OKR.Communication.Response;

namespace OKR.Application.UseCases.Objetives.GetAll;

public interface IGetAllExpenseUseCase
{
  Task<ResponseListObjectiveJson> Execute();
}
