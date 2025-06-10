using AutoMapper;
using OKR.Communication.Requests;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.Update;

public class UpdateFeedbackUseCase : IUpdateFeedbackUseCase
{
  private readonly IFeedbackUpdateOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public UpdateFeedbackUseCase(IFeedbackUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Execute(Guid feedbackId, RequestRegisterFeedbackJson request)
  {
    Validator(request);

    FeedbackEntity? feedback = await _repository.GetFeedbackById(feedbackId);
    if (feedback == null)
    {
      throw new NotFoundException("Feedback not found");
    }

    FeedbackEntity result = _mapper.Map(source: request, destination: feedback);

    await _repository.Update(feedback: result);
    await _unitOfWork.Commit();
  }

  private void Validator(RequestRegisterFeedbackJson requestUpdate)
  {
    var validator = new FeedbackValidator();
    var result = validator.Validate(instance: requestUpdate);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
