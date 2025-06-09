using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Repositories.KeyResults;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Action.GetActionByKeyResultId;

public class GetActionsByKeyResultIdUseCase : IGetActionsByKeyResultIdUseCase
{
  private readonly IKeyResultReadOnlyRepository _keyResultRepository;
  private readonly IActionReadOnlyRepository _actionRepository;
  private readonly IMapper _mapper;

  public GetActionsByKeyResultIdUseCase(IKeyResultReadOnlyRepository keyResultRepository, IActionReadOnlyRepository actionRepository, IMapper mapper)
  {
    _keyResultRepository = keyResultRepository;
    _actionRepository = actionRepository;
    _mapper = mapper;
  }

  public async Task<ResponseListActionJson> Execute(Guid keyResultId)
  {
    KeyResultEntity? keyResult = await _keyResultRepository.GetById(id: keyResultId);
    if (keyResult == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.KEY_RESULT_NOT_FOUND);
    }

    List<ActionEntity>? response = await _actionRepository.GetActionsByKeyResultId(keyResultId);
    if (response == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.ACTION_NOT_FOUND);
    }

    return new ResponseListActionJson
    {
      ListActions = _mapper.Map<List<ResponseActionJson>>(source: response)
    };
  }
}
