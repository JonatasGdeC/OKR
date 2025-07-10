using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.Action;

public class ActionValidator : AbstractValidator<RequestRegisterActionJson>
{
  public ActionValidator()
  {
    RuleFor(expression: action => action.Description).NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.DESCRIPTION_IS_REQUIRED).MinimumLength(minimumLength: 3).WithMessage(errorMessage: ResourceErrorMessage.DESCRIPTION_MINIMUM_CHARACTERS).MaximumLength(maximumLength: 1000).WithMessage(errorMessage: ResourceErrorMessage.DESCRIPTION_MAXIMUM_CHARACTERS);
    RuleFor(expression: action => action.ActionWeek).MaximumLength(maximumLength: 1000).WithMessage(errorMessage: ResourceErrorMessage.ACTION_WEEK_MAXIMUM_CHARACTERS);
    RuleFor(expression: action => action.Notes).MaximumLength(maximumLength: 1000).WithMessage(errorMessage: ResourceErrorMessage.ACTION_NOTES_MAXIMUM_CHARACTERS);
    RuleFor(expression: action => action.EndDate).Must(predicate: (action, endDate) => endDate >= action.StartDate).WithMessage(errorMessage: ResourceErrorMessage.ACTION_END_DATE_INVALID);
  }
}
