using FluentValidation;
using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Objetives.Register;

public class RegisterObjectiveValidator : AbstractValidator<RequestObjectiveJson>
{
  public RegisterObjectiveValidator()
  {
    RuleFor(objective => objective.Title).NotEmpty().WithName("Title is required.");
    RuleFor(objective => objective.Year).NotEmpty().WithName("Year is required.");
    RuleFor(objective => objective.Quarter).NotEmpty().WithName("Quarter is required.");
  }
}
