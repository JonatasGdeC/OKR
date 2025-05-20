using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.KeyResult.Register;

public class RegisterKeyResultValidator : AbstractValidator<RequestRegisterKeyResultJson>
{
  public RegisterKeyResultValidator()
  {
    RuleFor(expression: objective => objective.Title).NotEmpty().WithName(overridePropertyName: ResourceErrorMessage.TITLE_IS_REQUIRED).MinimumLength(minimumLength: 3).WithMessage(errorMessage: ResourceErrorMessage.TITLE_MINIMUM_CHARACTERS).MaximumLength(maximumLength: 500).WithMessage(errorMessage: ResourceErrorMessage.TITLE_MAXIMUM_CHARACTERS_IN_KEYRESULT);
    RuleFor(expression: objective => objective.NumberKr).NotEmpty().WithName(overridePropertyName: ResourceErrorMessage.NUMBER_KR_IS_REQUIRED).Must(predicate: number => number >= 1 && number <= 5).WithMessage(errorMessage: ResourceErrorMessage.KR_VALID);
  }
}
