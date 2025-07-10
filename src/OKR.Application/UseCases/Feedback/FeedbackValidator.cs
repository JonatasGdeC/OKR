using FluentValidation;
using OKR.Communication.Requests;
using OKR.Exception;

namespace OKR.Application.UseCases.Feedback;

public class FeedbackValidator : AbstractValidator<RequestRegisterFeedbackJson>
{
  public FeedbackValidator()
  {
    RuleFor(expression: feedback => feedback.Description).NotEmpty().WithMessage(errorMessage: ResourceErrorMessage.DESCRIPTION_IS_REQUIRED).MinimumLength(minimumLength: 3).WithMessage(errorMessage: ResourceErrorMessage.DESCRIPTION_MINIMUM_CHARACTERS).MaximumLength(maximumLength: 1000).WithMessage(errorMessage: ResourceErrorMessage.DESCRIPTION_MAXIMUM_CHARACTERS);
    RuleFor(expression: feedback => feedback.Date).Must(predicate: date => date.Date >= DateTime.Today.AddDays(value: -2) && date.Date <= DateTime.Today).WithMessage(errorMessage: "The feedback date must be today, yesterday, or the day before yesterday.");
  }
}
