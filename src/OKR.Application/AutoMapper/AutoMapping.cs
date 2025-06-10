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
    CreateMap<RequestRegisterObjectiveJson, ObjectiveEntity>();
    CreateMap<RequestUpdateObjectiveJson, ObjectiveEntity>();
    CreateMap<RequestRegisterKeyResultJson, KeyResultEntity>();
    CreateMap<RequestRegisterActionJson, ActionEntity>();
    CreateMap<RequestRegisterFeedbackJson, FeedbackEntity>();
  }

  private void EntityToResponse()
  {
    CreateMap<ObjectiveEntity, ResponseObjectiveJson>();
    CreateMap<KeyResultEntity, ResponseKeyResultJson>();
    CreateMap<ActionEntity, ResponseActionJson>();
    CreateMap<FeedbackEntity, ResponseFeedbackJson>();
  }
}
