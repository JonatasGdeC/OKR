using AutoMapper;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Secury;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
  private readonly IMapper _mapper;
  private readonly IPasswordEncripter  _passwordEncripter;

  public RegisterUserUseCase(IMapper mapper, IPasswordEncripter passwordEncripter)
  {
    _mapper = mapper;
    _passwordEncripter = passwordEncripter;
  }

  public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
  {
    Validate(request);

    var user = _mapper.Map<Domain.Entities.User>(request);
    user.Password = _passwordEncripter.Encrypt(request.Password);

    return new ResponseRegisteredUserJson
    {
      Name = user.Name,
      Email = user.Email,
    };
  }

  private void Validate(RequestRegisterUserJson request)
  {
    var result = new RegisterUserValidator().Validate(request);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
