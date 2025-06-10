using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.Feedback;

public class FeedbackValidator : AbstractValidator<RequestRegisterFeedbackJson>
{
  public FeedbackValidator()
  {
    RuleFor(expression: feedback => feedback.Description).NotEmpty().WithMessage(ResourceErrorMessage.DESCRIPTION_IS_REQUIRED).MinimumLength(3).WithMessage(ResourceErrorMessage.DESCRIPTION_MINIMUM_CHARACTERS).MaximumLength(1000).WithMessage(ResourceErrorMessage.DESCRIPTION_MAXIMUM_CHARACTERS);
    RuleFor(feedback => feedback.Date).Must(date => date.Date >= DateTime.Today.AddDays(-2) && date.Date <= DateTime.Today).WithMessage("The feedback date must be today, yesterday, or the day before yesterday.");
  }
}
