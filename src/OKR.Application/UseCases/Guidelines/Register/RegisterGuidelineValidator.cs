using FluentValidation;
using OKR.Communication.Requests;

namespace OKR.Application.UseCases.Guidelines.Register;

public class RegisterGuidelineValidator : AbstractValidator<RequestRegisterGuideline>
{
  public RegisterGuidelineValidator()
  {
    RuleFor(guideline => guideline.Title).NotEmpty().WithMessage("Title is required");
    RuleFor(guideline => guideline.Description).NotEmpty().WithMessage("Description is required");
    RuleFor(guideline => guideline.Type).IsInEnum().WithMessage("Type of guideline is invalid");
  }
}
