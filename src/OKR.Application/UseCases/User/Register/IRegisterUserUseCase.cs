using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
  Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
