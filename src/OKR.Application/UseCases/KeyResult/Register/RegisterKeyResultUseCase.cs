using AutoMapper;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.KeyResults;
using OKR.Domain.Repositories.Objectives;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.KeyResult.Register;

public class RegisterKeyResultUseCase : IRegisterKeyResultUseCase
{
  private readonly IObjetiveUpdateOnlyRepository _repositoryObjetive;
  private readonly IKeyResultReadOnlyRepository _repositoryReadKeyResult;
  private readonly IKeyResultWriteOnlyRepository _repositoryWriteKeyResult;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILoggedUser _loggedUser;

  public RegisterKeyResultUseCase(IObjetiveUpdateOnlyRepository repositoryObjetive, IKeyResultReadOnlyRepository repositoryReadKeyResult, IKeyResultWriteOnlyRepository repositoryKeyResult, IMapper mapper, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
  {
    _repositoryObjetive = repositoryObjetive;
    _repositoryReadKeyResult = repositoryReadKeyResult;
    _repositoryWriteKeyResult = repositoryKeyResult;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
    _loggedUser = loggedUser;
  }


  public async Task<ResponseKeyResultJson> Execute(RequestRegisterKeyResultJson requestRegister)
  {
    Validate(requestRegister: requestRegister);
    var loggedUser = await _loggedUser.Get();
    ObjectiveEntity? objective = await _repositoryObjetive.GetById(loggedUser: loggedUser, id: requestRegister.ObjectiveId);
    if (objective == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.OBJECTIVE_NOT_FOUND);
    }

    List<KeyResultEntity> list = await _repositoryReadKeyResult.GetKeyResultsByObjectiveId(loggedUser: loggedUser, id: requestRegister.ObjectiveId) ?? [];
    bool listWasCountFive = list.Count == 5;

    if (listWasCountFive)
    {
      throw new BadRequestException(message: ResourceErrorMessage.OBJECTIVE_HAS_5_KEY_RESULT);
    }

    bool listHasNumberKr = list.Exists(match: kr => kr.NumberKr == requestRegister.NumberKr);
    if (listHasNumberKr)
    {
      throw new BadRequestException(message: ResourceErrorMessage.KEY_RESULT_NUMBER_ALREADY_EXISTS);
    }

    bool listHasTitle = list.Exists(match: kr => kr.Title == requestRegister.Title);
    if (listHasTitle)
    {
      throw new BadRequestException(message: ResourceErrorMessage.KEY_RESULT_TITLE_ALREADY_EXISTS);
    }

    var entity = _mapper.Map<KeyResultEntity>(source: requestRegister);
    entity.UserId = loggedUser.Id;
    await _repositoryWriteKeyResult.Add(keyResult: entity);
    await _unitOfWork.Commit();
    return _mapper.Map<ResponseKeyResultJson>(source: entity);
  }

  private void Validate(RequestRegisterKeyResultJson requestRegister)
  {
    var validator = new RegisterKeyResultValidator();
    var result = validator.Validate(instance: requestRegister);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
