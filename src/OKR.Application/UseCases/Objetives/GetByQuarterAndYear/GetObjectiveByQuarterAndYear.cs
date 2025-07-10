using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Objectives;
using OKR.Domain.Services.LoggedUser;

namespace OKR.Application.UseCases.Objetives.GetByQuarterAndYear;

public class GetObjectiveByQuarterAndYear : IGetObjectiveByQuarterAndYear
{
  private readonly IObjetiveReadOnlyRepository _repository;
  private readonly ILoggedUser _loggedUser;
  private readonly IMapper _mapper;

  public GetObjectiveByQuarterAndYear(IObjetiveReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
  {
    _repository = repository;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseListObjectiveJson> Execute(int quarter, int year)
  {
    var loggedUser = await _loggedUser.Get();
    List<ObjectiveEntity> result = await _repository.GetByQuarterAndYear(loggedUser: loggedUser, quarter: quarter, year: year);

    return new ResponseListObjectiveJson
    {
      ListObjectives = _mapper.Map<List<ResponseObjectiveJson>>(source: result)
    };
  }
}
