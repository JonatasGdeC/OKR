using FluentValidation;
using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Action;

public class ActionValidator : AbstractValidator<RequestRegisterActionJson>
{
  public ActionValidator()
  {
    RuleFor(expression: action => action.Description).NotEmpty().WithMessage("Description is required").MinimumLength(3).WithMessage("Description must be greater than 3 characters").MaximumLength(1000).WithMessage("Description must be less than 1000 characters");
    RuleFor(expression: action => action.CurrentProgress).NotEmpty().WithMessage("Progress is required").Must(predicate: number => number >= 1 && number <= 100).WithMessage("Progress must be greater than 1 or less than 100");
    RuleFor(expression: action => action.ActionWeek).MaximumLength(1000).WithMessage("Action week cannot be longer than 1000 characters");
    RuleFor(expression: action => action.Notes).MaximumLength(1000).WithMessage("Notes cannot be longer than 1000 characters");
    RuleFor(action => action.EndDate).Must((action, endDate) => endDate >= action.StartDate).WithMessage("End date must be greater than or equal to start date.");
  }
}
