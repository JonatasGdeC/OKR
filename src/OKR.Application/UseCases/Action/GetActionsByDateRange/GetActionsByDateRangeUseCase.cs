using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Action.GetActionsByDateRange;

public class GetActionsByDateRangeUseCase : IGetActionsByDateRangeUseCase
{
  private readonly IActionReadOnlyRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public GetActionsByDateRangeUseCase(IActionReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
  {
    _repository = repository;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseListActionJson> Execute(DateTime startDate, DateTime endDate)
  {
    var loggedUser = await _loggedUser.Get();
    List<ActionEntity>? listActions = await _repository.GetActionsByDateRange(loggedUser: loggedUser, dateStart: startDate, dateEnd: endDate);
    if (listActions == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.ACTION_NOT_FOUND);
    }

    return new ResponseListActionJson
    {
      ListActions = _mapper.Map<List<ResponseActionJson>>(source: listActions)
    };
  }
}
