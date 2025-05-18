using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.Objetives.Update;

public class UpdateObjectiveValidator : AbstractValidator<RequestUpdateObjectiveJson>
{
  public UpdateObjectiveValidator()
  {
    RuleFor(objective => objective.Title).NotEmpty().WithName(ResourceErrorMessage.TITLE_IS_REQUIRED).MinimumLength(3).WithMessage(ResourceErrorMessage.TITLE_MINIMUM_CHARACTERS).MaximumLength(100).WithMessage(ResourceErrorMessage.TITLE_MAXIMUM_CHARACTERS);
  }
}
