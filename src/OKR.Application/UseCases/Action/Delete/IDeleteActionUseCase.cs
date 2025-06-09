namespace OKR.Application.UseCases.Action.Delete;

public interface IDeleteActionUseCase
{
  Task Execute(Guid actionId);
}
