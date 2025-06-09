using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.Application.UseCases.Action.Register;

public interface IRegisterActionUseCase
{
  Task<ResponseActionJson> Execute(RequestRegisterActionJson requestRegister);
}
