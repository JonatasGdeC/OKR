using AutoMapper;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.GetFeedbacksByDateRange;

public class GetFeedbacksByDateRangeUseCase : IGetFeedbacksByDateRangeUseCase
{
  private readonly IFeedbackReadOnlyRepository _repository;
  private readonly IMapper _mapper;

  public GetFeedbacksByDateRangeUseCase(IFeedbackReadOnlyRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  public async Task<ResponseListFeedbacksJson> Execute(DateTime dateStart, DateTime dateEnd)
  {
    List<FeedbackEntity>? response = await _repository.GetFeedbacksByDateRange(dateStart, dateEnd);
    if (response == null)
    {
      throw new NotFoundException("Feedbacks not found");
    }

    return new ResponseListFeedbacksJson
    {
      ListFeedbacks = _mapper.Map<List<ResponseFeedbackJson>>(source: response)
    };
  }
}
