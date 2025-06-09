using AutoMapper;
using OKR.Communication.Requests;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Actions;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Action.Update;

public class UpdateActionUseCase : IUpdateActionUseCase
{
  private readonly IActionUpdateOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public UpdateActionUseCase(IActionUpdateOnlyRepository actionUpdateOnlyRepository, IUnitOfWork unitOfWork, IMapper mapper)
  {
    _repository = actionUpdateOnlyRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Execute(Guid actionId, RequestRegisterActionJson request)
  {
    Validator(requestUpdate: request);

    ActionEntity? action = await _repository.GetById(actionId: actionId);
    if (action == null)
    {
      throw new NotFoundException(ResourceErrorMessage.ACTION_NOT_FOUND);
    }

    ActionEntity result = _mapper.Map(source: request, destination: action);

    await _repository.Update(action: result);
    await _unitOfWork.Commit();
  }

  private void Validator(RequestRegisterActionJson requestUpdate)
  {
    var validator = new ActionValidator();
    var result = validator.Validate(instance: requestUpdate);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
