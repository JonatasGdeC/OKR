using FluentValidation;
using OKR.Communication.Requests;

namespace OKR.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
  public RegisterUserValidator()
  {
    RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required.");
    RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Email is invalid.");
    RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
  }
}
