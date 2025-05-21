using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.Objetives.Register;

public class RegisterObjectiveValidator : AbstractValidator<RequestRegisterObjectiveJson>
{
  public RegisterObjectiveValidator()
  {
    RuleFor(expression: objective => objective.Title).NotEmpty().WithName(overridePropertyName: ResourceErrorMessage.TITLE_IS_REQUIRED).MinimumLength(minimumLength: 3).WithMessage(errorMessage: ResourceErrorMessage.TITLE_MINIMUM_CHARACTERS).MaximumLength(maximumLength: 100).WithMessage(errorMessage: ResourceErrorMessage.OBJECTIVE_TITLE_MAXIMUM_CHARACTERS);
    RuleFor(expression: objective => objective.Year).NotEmpty().WithName(overridePropertyName: ResourceErrorMessage.OBJECTIVE_YEAR_IS_REQUIRED).Must(predicate: year => year >= DateTime.Now.Year).WithMessage(errorMessage: ResourceErrorMessage.OBJECTIVE_YEAR_INVALID);
    RuleFor(expression: objective => objective.Quarter).NotEmpty().WithName(overridePropertyName: ResourceErrorMessage.OBJECTIVE_QUARTER_IS_REQUIRED).InclusiveBetween(from: 1, to: 4).WithMessage(errorMessage: ResourceErrorMessage.OBJECTIVE_QUARTER_INVALID);;
  }
}
