using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.Action;

public class ActionValidator : AbstractValidator<RequestRegisterActionJson>
{
  public ActionValidator()
  {
    RuleFor(expression: action => action.Description).NotEmpty().WithMessage(ResourceErrorMessage.DESCRIPTION_IS_REQUIRED).MinimumLength(3).WithMessage(ResourceErrorMessage.DESCRIPTION_MINIMUM_CHARACTERS).MaximumLength(1000).WithMessage(ResourceErrorMessage.DESCRIPTION_MAXIMUM_CHARACTERS);
    RuleFor(expression: action => action.ActionWeek).MaximumLength(1000).WithMessage(ResourceErrorMessage.ACTION_WEEK_MAXIMUM_CHARACTERS);
    RuleFor(expression: action => action.Notes).MaximumLength(1000).WithMessage(ResourceErrorMessage.ACTION_NOTES_MAXIMUM_CHARACTERS);
    RuleFor(action => action.EndDate).Must((action, endDate) => endDate >= action.StartDate).WithMessage(ResourceErrorMessage.ACTION_END_DATE_INVALID);
  }
}
