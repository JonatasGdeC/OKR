using OKR.Communication.Response;

namespace OKR.Application.UseCases.Action.GetActionByKeyResultId;

public interface IGetActionsByKeyResultIdUseCase
{
  Task<ResponseListActionJson> Execute(Guid keyResultId);
}
