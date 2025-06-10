using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Action.UpdateProgress;

public interface IUpdateProgressActionUseCase
{
  Task Execute(Guid actionId, RequestUpdateProgressActionJson requestProgressAction);
}
