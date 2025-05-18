using FluentValidation;
using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Objetives.Update;

public class UpdateObjectiveValidator : AbstractValidator<RequestUpdateObjectiveJson>
{
  public UpdateObjectiveValidator()
  {
    RuleFor(objective => objective.Title).NotEmpty().WithName("Title is required.").MinimumLength(3).WithMessage("Title must have at least 3 characters").MaximumLength(100).WithMessage("Title must have a maximum of 25 characters");
  }
}
