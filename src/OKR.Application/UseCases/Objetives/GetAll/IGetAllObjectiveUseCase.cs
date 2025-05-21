using OKR.Communication.Response;

namespace OKR.Application.UseCases.Objetives.GetAll;

public interface IGetAllObjectiveUseCase
{
  Task<ResponseListObjectiveJson> Execute();
}
