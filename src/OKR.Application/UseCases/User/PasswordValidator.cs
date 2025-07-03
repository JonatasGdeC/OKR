using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace OKR.Application.UseCases.User;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
  private const string ErrorMessage = "ErrorMessage";

  protected override string GetDefaultMessageTemplate(string errorCode)
  {
    return "{ErrorMessage}";
  }

  public override string Name => "PasswordValidator";

  public override bool IsValid(ValidationContext<T> context, string password)
  {
    if (string.IsNullOrEmpty(password))
    {
      context.MessageFormatter.AppendArgument(ErrorMessage, "Password is required.");
      return false;
    }

    if (password.Length < 8)
    {
      context.MessageFormatter.AppendArgument(ErrorMessage, "Password must be at least 8 characters.");
      return false;
    }

    if (!Regex.IsMatch(password, @"[A-Z]+"))
    {
      context.MessageFormatter.AppendArgument(ErrorMessage, "Password must have a capital letter.");
      return false;
    }

    if (!Regex.IsMatch(password, @"[a-z]+"))
    {
      context.MessageFormatter.AppendArgument(ErrorMessage, "Password must have a lowercase letter.");
      return false;
    }

    if (!Regex.IsMatch(password, @"[0-9]+"))
    {
      context.MessageFormatter.AppendArgument(ErrorMessage, "Password must have a digit");
      return false;
    }

    if (!Regex.IsMatch(password, @"[\w\.\-]+"))
    {
      context.MessageFormatter.AppendArgument(ErrorMessage, "Password must have a special character.");
      return false;
    }

    return true;
  }
}
