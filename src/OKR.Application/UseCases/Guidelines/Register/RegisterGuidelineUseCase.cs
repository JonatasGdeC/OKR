using OKR.Communication.Enums;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Guidelines.Register;

public class RegisterGuidelineUseCase
{
  public ResponseRegisterGuidelineJson Execute(RequestRegisterGuideline request)
  {
    Validate(request);
    // return new ResponseRegisterGuidelineJson()

    // Simulation
    return new ResponseRegisterGuidelineJson
    {
      Id = Guid.NewGuid(),
      Title = request.Title,
      Type = request.Type,
      Description = request.Description,
      CreatedAt = DateTime.Now,
    };
  }

  private void Validate(RequestRegisterGuideline request)
  {
    var validator = new RegisterGuidelineValidator();
    var result = validator.Validate(request);

    if (!result.IsValid)
    {
      var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errors);
    }
  }
}
