using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Objectives;

namespace OKR.Application.UseCases.Objetives.GetByQuarterAndYear;

public class GetObjectiveByQuarterAndYear : IGetObjectiveByQuarterAndYear
{
  private readonly IObjetiveReadOnlyRepository _repository;
  private readonly IMapper _mapper;

  public GetObjectiveByQuarterAndYear(IObjetiveReadOnlyRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<ResponseListObjectiveJson> Execute(int quarter, int year)
  {
    List<Objective> result = await _repository.GetByQuarterAndYear(quarter, year);

    return new ResponseListObjectiveJson
    {
      ListObjectives = _mapper.Map<List<ResponseObjectiveJson>>(source: result)
    };
  }
}
