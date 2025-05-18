using FluentValidation;
using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Objetives.Register;

public class ObjectiveValidator : AbstractValidator<RequestObjectiveJson>
{
  public ObjectiveValidator()
  {
    RuleFor(objective => objective.Title).NotEmpty().WithName("Title is required.").MinimumLength(3).WithMessage("Title must have at least 3 characters").MaximumLength(100).WithMessage("Title must have a maximum of 25 characters");
    RuleFor(objective => objective.Year).NotEmpty().WithName("Year is required.").Must(year => year >= DateTime.Now.Year).WithMessage("Year must be the current year or later.");;
    RuleFor(objective => objective.Quarter).NotEmpty().WithName("Quarter is required.").InclusiveBetween(1, 4).WithMessage("Quarter must be between 1 and 4.");;
  }
}
