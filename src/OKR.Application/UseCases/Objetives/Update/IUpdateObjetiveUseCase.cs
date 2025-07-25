using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.Application.UseCases.Objetives.Update;

public interface IUpdateObjetiveUseCase
{
  Task Execute(Guid id, RequestUpdateObjectiveJson requestRegister);
}
