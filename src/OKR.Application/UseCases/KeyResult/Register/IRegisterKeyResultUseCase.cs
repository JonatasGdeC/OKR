using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.Application.UseCases.KeyResult.Register;

public interface IRegisterKeyResultUseCase
{
  Task<ResponseKeyResultJson> Execute(RequestRegisterKeyResultJson requestRegister);
}
