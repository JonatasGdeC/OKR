using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.GetFeedbacksByDateRange;

public class GetFeedbacksByDateRangeUseCase : IGetFeedbacksByDateRangeUseCase
{
  private readonly IFeedbackReadOnlyRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public GetFeedbacksByDateRangeUseCase(IFeedbackReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
  {
    _repository = repository;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseListFeedbacksJson> Execute(DateTime dateStart, DateTime dateEnd)
  {
    var loggedUser = await _loggedUser.Get();
    List<FeedbackEntity>? response = await _repository.GetFeedbacksByDateRange(loggedUser: loggedUser, dateStart: dateStart, dateEnd: dateEnd);
    if (response == null)
    {
      throw new NotFoundException(message: "Feedbacks not found");
    }

    return new ResponseListFeedbacksJson
    {
      ListFeedbacks = _mapper.Map<List<ResponseFeedbackJson>>(source: response)
    };
  }
}
