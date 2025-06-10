using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.GetFeedbacksByAction;

public class GetFeedbacksByActionIdUseCase : IGetFeedbacksByActionIdUseCase
{
  private readonly IActionReadOnlyRepository _actionRepository;
  private readonly IFeedbackReadOnlyRepository _feedbackRepository;
  private readonly IMapper _mapper;

  public GetFeedbacksByActionIdUseCase(IActionReadOnlyRepository actionRepository, IFeedbackReadOnlyRepository feedbackRepository, IMapper mapper)
  {
    _actionRepository = actionRepository;
    _feedbackRepository = feedbackRepository;
    _mapper = mapper;
  }


  public async Task<ResponseListFeedbacksJson> Execute(Guid actionId)
  {
    ActionEntity? action = await _actionRepository.GetActionById(actionId);
    if (action == null)
    {
      throw new NotFoundException(ResourceErrorMessage.ACTION_NOT_FOUND);
    }

    List<FeedbackEntity>? response = await _feedbackRepository.GetFeedbacksByActionId(actionId);
    if (response == null)
    {
      throw new NotFoundException(ResourceErrorMessage.ACTION_NOT_FOUND);
    }

    return new ResponseListFeedbacksJson
    {
      ListFeedbacks = _mapper.Map<List<ResponseFeedbackJson>>(source: response)
    };
  }
}
