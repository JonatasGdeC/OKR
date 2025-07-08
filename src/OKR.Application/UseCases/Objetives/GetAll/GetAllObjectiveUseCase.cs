using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Objectives;
using OKR.Domain.Services.LoggedUser;

namespace OKR.Application.UseCases.Objetives.GetAll;

public class GetAllObjectiveUseCase : IGetAllObjectiveUseCase
{
  private readonly IObjetiveReadOnlyRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public GetAllObjectiveUseCase(IObjetiveReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
  {
    _repository = repository;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseListObjectiveJson> Execute()
  {
    var loggedUser = await _loggedUser.Get();
    List<ObjectiveEntity> result = await _repository.GetAll(loggedUser);

    return new ResponseListObjectiveJson
    {
      ListObjectives = _mapper.Map<List<ResponseObjectiveJson>>(source: result)
    };
  }
}
