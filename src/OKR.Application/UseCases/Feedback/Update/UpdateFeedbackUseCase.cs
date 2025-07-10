using AutoMapper;
using OKR.Communication.Requests;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.Update;

public class UpdateFeedbackUseCase : IUpdateFeedbackUseCase
{
  private readonly IFeedbackUpdateOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public UpdateFeedbackUseCase(
    IFeedbackUpdateOnlyRepository repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task Execute(Guid feedbackId, RequestRegisterFeedbackJson request)
  {
    Validator(requestUpdate: request);
    var loggedUser = await _loggedUser.Get();
    FeedbackEntity? feedback = await _repository.GetFeedbackById(loggedUser: loggedUser, feedbackId: feedbackId);
    if (feedback == null)
    {
      throw new NotFoundException(message: "Feedback not found");
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
