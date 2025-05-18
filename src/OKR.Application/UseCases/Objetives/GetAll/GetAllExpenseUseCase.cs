using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Objectives;

namespace OKR.Application.UseCases.Objetives.GetAll;

public class GetAllExpenseUseCase : IGetAllExpenseUseCase
{
  private readonly IObjetiveReadOnlyRepository _repository;
  private readonly IMapper _mapper;

  public GetAllExpenseUseCase(IObjetiveReadOnlyRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<ResponseListObjectiveJson> Execute()
  {
    List<Objective> result = await _repository.GetAll();

    return new ResponseListObjectiveJson
    {
      ListObjectives = _mapper.Map<List<ResponseObjectiveJson>>(source: result)
    };
  }
}
