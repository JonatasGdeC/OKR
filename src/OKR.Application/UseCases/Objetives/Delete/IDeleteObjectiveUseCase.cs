namespace OKR.Application.UseCases.Objetives.Delete;

public interface IDeleteObjectiveUseCase
{
  Task Execute(Guid id);
}
