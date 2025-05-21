namespace OKR.Application.UseCases.KeyResult.Delete;

public interface IDeleteKeyResultUseCase
{
  Task Execute(Guid id);
}
