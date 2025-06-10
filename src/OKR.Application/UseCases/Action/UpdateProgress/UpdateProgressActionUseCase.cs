using OKR.Communication.Requests;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Actions;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Action.UpdateProgress;

public class UpdateProgressActionUseCase : IUpdateProgressActionUseCase
{
  private readonly IActionUpdateOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateProgressActionUseCase(IActionUpdateOnlyRepository actionUpdateOnlyRepository, IUnitOfWork unitOfWork)
  {
    _repository = actionUpdateOnlyRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(Guid actionId, RequestUpdateProgressActionJson requestProgressAction)
  {
    Validator(requestProgressAction: requestProgressAction);

    ActionEntity? action = await _repository.GetById(actionId: actionId);
    if (action == null)
    {
      throw new NotFoundException(ResourceErrorMessage.ACTION_NOT_FOUND);
    }


    await _repository.UpdateProgress(action: action, progress: requestProgressAction.CurrentProgress);
    await _unitOfWork.Commit();
  }

  private void Validator(RequestUpdateProgressActionJson requestProgressAction)
  {
    var validator = new UpdateProgressActionValidator();
    var result = validator.Validate(requestProgressAction);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
