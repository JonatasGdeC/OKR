using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.Objetives.Register;

public class RegisterObjectiveValidator : AbstractValidator<RequestRegisterObjectiveJson>
{
  public RegisterObjectiveValidator()
  {
    RuleFor(objective => objective.Title).NotEmpty().WithName(ResourceErrorMessage.TITLE_IS_REQUIRED).MinimumLength(3).WithMessage(ResourceErrorMessage.TITLE_MINIMUM_CHARACTERS).MaximumLength(100).WithMessage(ResourceErrorMessage.TITLE_MAXIMUM_CHARACTERS);
    RuleFor(objective => objective.Year).NotEmpty().WithName(ResourceErrorMessage.YEAR_IS_REQUIRED).Must(year => year >= DateTime.Now.Year).WithMessage(ResourceErrorMessage.YEAR_VALID);
    RuleFor(objective => objective.Quarter).NotEmpty().WithName(ResourceErrorMessage.QUARTER_IS_REQUIRED).InclusiveBetween(1, 4).WithMessage(ResourceErrorMessage.QUARTER_VALID);;
  }
}
