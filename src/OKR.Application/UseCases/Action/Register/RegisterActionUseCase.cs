using AutoMapper;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Repositories.KeyResults;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Action.Register;

public class RegisterActionUseCase : IRegisterActionUseCase
{
  private readonly IKeyResultReadOnlyRepository _keyResultRepository;
  private readonly IActionWriteOnlyRepository _actionRepository;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILoggedUser _loggedUser;

  public RegisterActionUseCase(IKeyResultReadOnlyRepository keyResultRepository, IActionWriteOnlyRepository actionRepository, IMapper mapper, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
  {
    _keyResultRepository = keyResultRepository;
    _actionRepository = actionRepository;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseActionJson> Execute(RequestRegisterActionJson requestRegister)
  {
    Validator(requestRegister: requestRegister);
    var loggedUser = await _loggedUser.Get();
    KeyResultEntity? keyResult = await _keyResultRepository.GetById(loggedUser: loggedUser, id: requestRegister.KeyResultId);
    if (keyResult == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.KEY_RESULT_NOT_FOUND);
    }

    var entity = _mapper.Map<ActionEntity>(source: requestRegister);
    entity.UserId = loggedUser.Id;

    await _actionRepository.Add(action: entity);
    await _unitOfWork.Commit();
    return _mapper.Map<ResponseActionJson>(source: entity);
  }

  private void Validator(RequestRegisterActionJson requestRegister)
  {
    var validator = new ActionValidator();
    var result = validator.Validate(instance: requestRegister);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
