using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
  Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
