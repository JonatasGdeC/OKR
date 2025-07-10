using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.KeyResults;
using OKR.Domain.Repositories.Objectives;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.KeyResult.GetById;

public class GetKeyResultByObjectiveId : IGetKeyResultByIdUseCase
{
  private readonly IObjetiveUpdateOnlyRepository _repositoryObjetive;
  private readonly IKeyResultReadOnlyRepository _repositoryKeyResult;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public GetKeyResultByObjectiveId(IObjetiveUpdateOnlyRepository repositoryObjetive, IKeyResultReadOnlyRepository repositoryKeyResult, IMapper mapper, ILoggedUser loggedUser)
  {
    _repositoryObjetive = repositoryObjetive;
    _repositoryKeyResult = repositoryKeyResult;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseListKeyResultJson> Execute(Guid id)
  {
    var loggedUser = await _loggedUser.Get();
    ObjectiveEntity? objetive = await _repositoryObjetive.GetById(loggedUser: loggedUser, id: id);
    if (objetive == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.OBJECTIVE_NOT_FOUND);
    }

    List<KeyResultEntity>? response = await _repositoryKeyResult.GetKeyResultsByObjectiveId(loggedUser: loggedUser, id: id);
    if (response == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.KEY_RESULT_NOT_FOUND);
    }

    return new ResponseListKeyResultJson
    {
      ListKeyResults = _mapper.Map<List<ResponseKeyResultJson>>(source: response)
    };
  }
}
