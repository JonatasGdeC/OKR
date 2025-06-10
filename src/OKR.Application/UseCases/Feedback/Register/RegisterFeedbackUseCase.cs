using AutoMapper;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Actions;
using OKR.Domain.Repositories.Feedbacks;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Feedback.Register;

public class RegisterFeedbackUseCase : IRegisterFeedbackUseCase
{
  private readonly IFeedbackWriteOnlyRepository _feedbackRepository;
  private readonly IActionReadOnlyRepository _actionRepository;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;

  public RegisterFeedbackUseCase(IFeedbackWriteOnlyRepository repository, IActionReadOnlyRepository actionRepository, IMapper mapper, IUnitOfWork unitOfWork)
  {
    _feedbackRepository = repository;
    _actionRepository = actionRepository;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
  }

  public async Task<ResponseFeedbackJson> Execute(RequestRegisterFeedbackJson request)
  {
    Validator(request);

    ActionEntity? action = await _actionRepository.GetActionById(actionId: request.ActionId);
    if (action == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.KEY_RESULT_NOT_FOUND);
    }

    var entity = _mapper.Map<FeedbackEntity>(source: request);

    await _feedbackRepository.AddFeedback(entity);
    await _unitOfWork.Commit();
    return _mapper.Map<ResponseFeedbackJson>(source: entity);
  }

  private void Validator(RequestRegisterFeedbackJson request)
  {
    var validator = new FeedbackValidator();
    var result = validator.Validate(request);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
