using OKR.Communication.Response;

namespace OKR.Application.UseCases.KeyResult.GetById;

public interface IGetKeyResultByIdUseCase
{
  Task<ResponseListKeyResultJson> Execute(Guid id);
}
