using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.KeyResult;

public class RegisterKeyResultValidator : AbstractValidator<RequestRegisterKeyResultJson>
{
  public RegisterKeyResultValidator()
  {
    RuleFor(expression: objective => objective.Title).NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.TITLE_IS_REQUIRED).MinimumLength(minimumLength: 3).WithMessage(errorMessage: ResourceErrorMessage.TITLE_MINIMUM_CHARACTERS).MaximumLength(maximumLength: 500).WithMessage(errorMessage: ResourceErrorMessage.KEY_RESULT_TITLE_MAXIMUM_CHARACTERS);
    RuleFor(expression: objective => objective.NumberKr).NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.KEY_RESULT_NUMBER_IS_REQUIRED).Must(predicate: number => number >= 1 && number <= 5).WithMessage(errorMessage: ResourceErrorMessage.KEY_RESULT_NUMBER_INVALID);
  }
}
