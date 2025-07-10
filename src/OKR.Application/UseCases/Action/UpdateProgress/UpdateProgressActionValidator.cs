using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.Action.UpdateProgress;

public class UpdateProgressActionValidator : AbstractValidator<RequestUpdateProgressActionJson>
{
  public UpdateProgressActionValidator()
  {
    RuleFor(expression: action => action.CurrentProgress).Must(predicate: number => number >= 0 && number <= 100).WithMessage(errorMessage: ResourceErrorMessage.ACTION_PROGRESS_INVALID);
  }
}
