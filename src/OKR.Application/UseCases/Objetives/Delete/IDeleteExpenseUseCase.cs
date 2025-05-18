namespace OKR.Application.UseCases.Objetives.Delete;

public interface IDeleteExpenseUseCase
{
  Task Execute(Guid id);
}
