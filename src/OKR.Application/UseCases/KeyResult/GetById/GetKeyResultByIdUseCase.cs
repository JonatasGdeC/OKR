using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.KeyResults;
using OKR.Domain.Repositories.Objectives;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.KeyResult.GetById;

public class GetKeyResultByObjectiveId : IGetKeyResultByIdUseCase
{
  private readonly IObjetiveUpdateOnlyRepository _repositoryObjetive;
  private readonly IKeyResultReadOnlyRepository _repositoryKeyResult;
  private readonly IMapper _mapper;

  public GetKeyResultByObjectiveId(IObjetiveUpdateOnlyRepository repositoryObjetive, IKeyResultReadOnlyRepository repositoryKeyResult, IMapper mapper)
  {
    _repositoryObjetive = repositoryObjetive;
    _repositoryKeyResult = repositoryKeyResult;
    _mapper = mapper;
  }

  public async Task<ResponseListKeyResultJson> Execute(Guid id)
  {
    Objective? objetive = await _repositoryObjetive.GetById(id: id);
    if (objetive == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.OBJECTIVE_NOT_FOUND);
    }

    List<Domain.Entities.KeyResult>? response = await _repositoryKeyResult.GetKeyResultsByObjectiveId(id: id);
    if (response == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.OBJECTIVE_NOT_FOUND);
    }

    return new ResponseListKeyResultJson
    {
      ListKeyResults = _mapper.Map<List<ResponseKeyResultJson>>(source: response)
    };
  }
}
