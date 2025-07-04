using AutoMapper;
using FluentValidation.Results;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Repositories.User;
using OKR.Domain.Secury;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
  private readonly IMapper _mapper;
  private readonly IPasswordEncripter  _passwordEncripter;
  private readonly IUserReadOnlyRepository _repository;

  public RegisterUserUseCase(IMapper mapper, IPasswordEncripter passwordEncripter, IUserReadOnlyRepository repository)
  {
    _mapper = mapper;
    _passwordEncripter = passwordEncripter;
    _repository = repository;
  }

  public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
  {
    await Validate(request);

    var user = _mapper.Map<Domain.Entities.User>(request);
    user.Password = _passwordEncripter.Encrypt(request.Password);

    return new ResponseRegisteredUserJson
    {
      Name = user.Name,
      Email = user.Email,
    };
  }

  private async Task Validate(RequestRegisterUserJson request)
  {
    var result = new RegisterUserValidator().Validate(request);
    bool emailExist = await _repository.ExistActiveUserWithEmail(request.Email);

    if (emailExist)
    {
      result.Errors.Add(new ValidationFailure(String.Empty,"Email already exists."));
    }

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
