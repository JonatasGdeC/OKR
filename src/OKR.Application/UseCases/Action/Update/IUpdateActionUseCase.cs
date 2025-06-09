using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Action.Update;

public interface IUpdateActionUseCase
{
  Task Execute(Guid actionId, RequestRegisterActionJson requestUpdate);
}
