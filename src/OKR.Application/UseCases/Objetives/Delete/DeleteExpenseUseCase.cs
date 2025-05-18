using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Objectives;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Objetives.Delete;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
  private readonly IObjectiveWriteOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteExpenseUseCase(IObjectiveWriteOnlyRepository repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(Guid id)
  {
    bool result = await _repository.Delete(id: id);

    if (!result)
    {
      throw new NotFoundException(message: ResourceErrorMessage.OBJECTIVE_NOT_FOUND);
    }

    await _unitOfWork.Commit();
  }
}
