using FluentValidation;
using OKR.Communication.Requests;

namespace OKR.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
  public RegisterUserValidator()
  {
    RuleFor(expression: user => user.Name).NotEmpty().WithMessage(errorMessage: "Name is required.");
    RuleFor(expression: user => user.Email).NotEmpty().WithMessage(errorMessage: "Email is required.").EmailAddress().WithMessage(errorMessage: "Email is invalid.");
    RuleFor(expression: user => user.Password).SetValidator(validator: new PasswordValidator<RequestRegisterUserJson>());
  }
}
