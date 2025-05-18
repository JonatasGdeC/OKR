using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Objetives.Update;

public interface IUpdateObjetiveUseCase
{
  Task Execute(Guid id, RequestObjectiveJson request);
}
