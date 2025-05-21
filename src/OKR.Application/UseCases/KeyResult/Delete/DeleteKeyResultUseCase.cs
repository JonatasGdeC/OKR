using OKR.Domain.Repositories;
using OKR.Domain.Repositories.KeyResults;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.KeyResult.Delete;

public class DeleteKeyResultUseCase : IDeleteKeyResultUseCase
{
  private readonly IKeyResultWriteOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteKeyResultUseCase(IKeyResultWriteOnlyRepository repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(Guid id)
  {
    bool result = await _repository.Delete(id: id);

    if (!result)
    {
      throw new NotFoundException(message: ResourceErrorMessage.KEY_RESULT_NOT_FOUND);
    }

    await _unitOfWork.Commit();
  }
}
