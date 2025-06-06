using AutoMapper;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.KeyResults;
using OKR.Domain.Repositories.Objectives;
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

  public RegisterKeyResultUseCase(IObjetiveUpdateOnlyRepository repositoryObjetive, IKeyResultReadOnlyRepository repositoryReadKeyResult, IKeyResultWriteOnlyRepository repositoryKeyResult, IMapper mapper, IUnitOfWork unitOfWork)
  {
    _repositoryObjetive = repositoryObjetive;
    _repositoryReadKeyResult = repositoryReadKeyResult;
    _repositoryWriteKeyResult = repositoryKeyResult;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
  }


  public async Task<ResponseKeyResultJson> Execute(RequestRegisterKeyResultJson requestRegister)
  {
    Validate(requestRegister);

    Objective? objective = await _repositoryObjetive.GetById(id: requestRegister.ObjectiveId);
    if (objective == null)
    {
      throw new NotFoundException(ResourceErrorMessage.OBJECTIVE_NOT_FOUND);
    }

    List<Domain.Entities.KeyResult>? list = await _repositoryReadKeyResult.GetKeyResultsByObjectiveId(requestRegister.ObjectiveId);
    if (list != null && list.Count == 5)
    {
      throw new NotFoundException(ResourceErrorMessage.OBJECTIVE_HAS_5_KEY_RESULT);
    }

    if (list != null && list.Exists(kr => kr.NumberKr == requestRegister.NumberKr))
    {
      throw new NotFoundException(ResourceErrorMessage.KEY_RESULT_NUMBER_ALREADY_EXISTS);
    }

    if (list != null && list.Exists(kr => kr.Title == requestRegister.Title))
    {
      throw new NotFoundException(ResourceErrorMessage.KEY_RESULT_TITLE_ALREADY_EXISTS);
    }

    var entity = _mapper.Map<Domain.Entities.KeyResult>(source: requestRegister);
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
