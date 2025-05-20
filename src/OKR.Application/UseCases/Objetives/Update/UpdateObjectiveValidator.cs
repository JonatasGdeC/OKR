using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.Objetives.Update;

public class UpdateObjectiveValidator : AbstractValidator<RequestUpdateObjectiveJson>
{
  public UpdateObjectiveValidator()
  {
    RuleFor(expression: objective => objective.Title).NotEmpty().WithName(overridePropertyName: ResourceErrorMessage.TITLE_IS_REQUIRED).MinimumLength(minimumLength: 3).WithMessage(errorMessage: ResourceErrorMessage.TITLE_MINIMUM_CHARACTERS).MaximumLength(maximumLength: 100).WithMessage(errorMessage: ResourceErrorMessage.TITLE_MAXIMUM_CHARACTERS_IN_OBJECTIVES);
  }
}
