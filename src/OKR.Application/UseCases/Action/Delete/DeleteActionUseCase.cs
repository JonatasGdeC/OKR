using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Actions;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Action.Delete;

public class DeleteActionUseCase : IDeleteActionUseCase
{
  private readonly IActionWriteOnlyRepository _actionWriteOnlyRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteActionUseCase(IActionWriteOnlyRepository actionWriteOnlyRepository, IUnitOfWork unitOfWork)
  {
    _actionWriteOnlyRepository = actionWriteOnlyRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(Guid actionId)
  {
    bool result = await _actionWriteOnlyRepository.Delete(actionId);

    if (!result)
    {
      throw new NotFoundException(ResourceErrorMessage.ACTION_NOT_FOUND);
    }

    await _unitOfWork.Commit();
  }
}
