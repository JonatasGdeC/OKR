using AutoMapper;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.Register;

public class RegisterFeedbackUseCase : IRegisterFeedbackUseCase
{
  private readonly IFeedbackWriteOnlyRepository _feedbackRepository;
  private readonly IActionReadOnlyRepository _actionRepository;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILoggedUser _loggedUser;

  public RegisterFeedbackUseCase(
    IFeedbackWriteOnlyRepository repository,
    IActionReadOnlyRepository actionRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser)
  {
    _feedbackRepository = repository;
    _actionRepository = actionRepository;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseFeedbackJson> Execute(RequestRegisterFeedbackJson request)
  {
    Validator(request: request);
    var loggedUser = await _loggedUser.Get();
    ActionEntity? action = await _actionRepository.GetActionById(loggedUser: loggedUser, actionId: request.ActionId);
    if (action == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.KEY_RESULT_NOT_FOUND);
    }

    var entity = _mapper.Map<FeedbackEntity>(source: request);
    entity.UserId = loggedUser.Id;

    await _feedbackRepository.AddFeedback(feedback: entity);
    await _unitOfWork.Commit();
    return _mapper.Map<ResponseFeedbackJson>(source: entity);
  }

  private void Validator(RequestRegisterFeedbackJson request)
  {
    var validator = new FeedbackValidator();
    var result = validator.Validate(instance: request);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
