using FluentValidation;
using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Guidelines.Register;

public class RegisterGuidelineValidator : AbstractValidator<RequestRegisterGuideline>
{
  public RegisterGuidelineValidator()
  {
    RuleFor(guideline => guideline.Title)
      .NotEmpty().WithMessage("Title is required")
      .MinimumLength(3).WithMessage("Title must have at least 3 characters")
      .MaximumLength(25).WithMessage("Title must have a maximum of 25 characters");

    RuleFor(guideline => guideline.Description)
      .NotEmpty().WithMessage("Description is required")
      .MinimumLength(3).WithMessage("Description must have at least 3 characters")
      .MaximumLength(50).WithMessage("Description must have a maximum of 25 characters");;

    RuleFor(guideline => guideline.Type).IsInEnum().WithMessage("Type of guideline is invalid");
  }
}
