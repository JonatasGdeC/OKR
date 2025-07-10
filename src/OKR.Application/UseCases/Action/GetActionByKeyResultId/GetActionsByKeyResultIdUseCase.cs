using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Repositories.KeyResults;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Action.GetActionByKeyResultId;

public class GetActionsByKeyResultIdUseCase : IGetActionsByKeyResultIdUseCase
{
  private readonly IKeyResultReadOnlyRepository _keyResultRepository;
  private readonly IActionReadOnlyRepository _actionRepository;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public GetActionsByKeyResultIdUseCase(IKeyResultReadOnlyRepository keyResultRepository, IActionReadOnlyRepository actionRepository, IMapper mapper, ILoggedUser loggedUser)
  {
    _keyResultRepository = keyResultRepository;
    _actionRepository = actionRepository;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseListActionJson> Execute(Guid keyResultId)
  {
    var loggedUser = await _loggedUser.Get();
    KeyResultEntity? keyResult = await _keyResultRepository.GetById(loggedUser: loggedUser, id: keyResultId);
    if (keyResult == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.KEY_RESULT_NOT_FOUND);
    }

    List<ActionEntity>? response = await _actionRepository.GetActionsByKeyResultId(loggedUser: loggedUser, keyResultId: keyResultId);
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
