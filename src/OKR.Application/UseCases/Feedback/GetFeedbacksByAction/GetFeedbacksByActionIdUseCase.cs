using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.GetFeedbacksByAction;

public class GetFeedbacksByActionIdUseCase : IGetFeedbacksByActionIdUseCase
{
  private readonly IActionReadOnlyRepository _actionReadOnlyRepository;
  private readonly IFeedbackReadOnlyRepository _feedbackRepository;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public GetFeedbacksByActionIdUseCase(
    IActionReadOnlyRepository actionReadOnlyRepository,
    IFeedbackReadOnlyRepository feedbackRepository,
    IMapper mapper,
    ILoggedUser loggedUser)
  {
    _actionReadOnlyRepository = actionReadOnlyRepository;
    _feedbackRepository = feedbackRepository;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }


  public async Task<ResponseListFeedbacksJson> Execute(Guid actionId)
  {
    var loggedUser = await _loggedUser.Get();
    ActionEntity? action = await _actionReadOnlyRepository.GetActionById(loggedUser: loggedUser, actionId: actionId);
    if (action == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.ACTION_NOT_FOUND);
    }

    List<FeedbackEntity>? response = await _feedbackRepository.GetFeedbacksByActionId(loggedUser: loggedUser, actionId: actionId);
    if (response == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.ACTION_NOT_FOUND);
    }

    return new ResponseListFeedbacksJson
    {
      ListFeedbacks = _mapper.Map<List<ResponseFeedbackJson>>(source: response)
    };
  }
}
