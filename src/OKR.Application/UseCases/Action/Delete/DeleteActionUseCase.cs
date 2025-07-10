using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Action.Delete;

public class DeleteActionUseCase : IDeleteActionUseCase
{
  private readonly IActionWriteOnlyRepository _actionWriteOnlyRepository;
  private readonly IActionReadOnlyRepository _actionReadOnlyRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILoggedUser _loggedUser;

  public DeleteActionUseCase(IActionWriteOnlyRepository actionWriteOnlyRepository, IActionReadOnlyRepository actionReadOnlyRepository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
  {
    _actionWriteOnlyRepository = actionWriteOnlyRepository;
    _actionReadOnlyRepository = actionReadOnlyRepository;
    _unitOfWork = unitOfWork;
    _loggedUser = loggedUser;
  }

  public async Task Execute(Guid actionId)
  {
    var loggedUser = await _loggedUser.Get();
    ActionEntity? action = await _actionReadOnlyRepository.GetActionById(loggedUser: loggedUser, actionId: actionId);

    if (action == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.ACTION_NOT_FOUND);
    }

    await _actionWriteOnlyRepository.Delete(id: actionId);
    await _unitOfWork.Commit();
  }
}
