using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Actions;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Action.GetActionsByDateRange;

public class GetActionsByDateRangeUseCase : IGetActionsByDateRangeUseCase
{
  private readonly IActionReadOnlyRepository _repository;
  private readonly IMapper _mapper;

  public GetActionsByDateRangeUseCase(IActionReadOnlyRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<ResponseListActionJson> Execute(DateTime startDate, DateTime endDate)
  {
    List<ActionEntity>? listActions = await _repository.GetActionsByDateRange(startDate, endDate);
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
