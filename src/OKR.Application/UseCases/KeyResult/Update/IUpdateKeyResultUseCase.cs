using OKR.Communication.Requests;

namespace OKR.Application.UseCases.KeyResult.Update;

public interface IUpdateKeyResultUseCase
{
  Task Execute(Guid id, RequestRegisterKeyResultJson request);
}
