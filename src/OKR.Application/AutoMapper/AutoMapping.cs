using AutoMapper;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Entities;

namespace OKR.Application.AutoMapper;

public class AutoMapping : Profile
{
  public AutoMapping()
  {
    RequestEntity();
    EntityToResponse();
  }

  private void RequestEntity()
  {
    CreateMap<RequestRegisterObjectiveJson, Objective>();
    CreateMap<RequestUpdateObjectiveJson, Objective>();
  }

  private void EntityToResponse()
  {
    CreateMap<Objective, ResponseObjectiveJson>();
    CreateMap<KeyResult, ResponseKeyResultJson>();
  }
}
