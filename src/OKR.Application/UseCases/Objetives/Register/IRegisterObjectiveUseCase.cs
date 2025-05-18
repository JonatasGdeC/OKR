using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.Application.UseCases.Objetives.Register;

public interface IRegisterObjectiveUseCase
{
  Task<ResponseObjectiveJson> Execute(RequestRegisterObjectiveJson requestRegister);
}
