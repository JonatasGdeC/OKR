using AutoMapper;
using FluentValidation.Results;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.User;
using OKR.Domain.Secury;
using OKR.Domain.Secury.Cryptography;
using OKR.Domain.Tokens;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
  private readonly IMapper _mapper;
  private readonly IPasswordEncripter _passwordEncripter;
  private readonly IUserReadOnlyRepository _userReadOnlyRepository;
  private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IAccessTokenGenerator _accessTokenGenerator;

  public RegisterUserUseCase(IMapper mapper, IPasswordEncripter passwordEncripter,
    IUserReadOnlyRepository userReadOnlyRepository, IUserWriteOnlyRepository userWriteOnlyRepository,
    IUnitOfWork unitOfWork, IAccessTokenGenerator accessTokenGenerator)
  {
    _mapper = mapper;
    _passwordEncripter = passwordEncripter;
    _userReadOnlyRepository = userReadOnlyRepository;
    _userWriteOnlyRepository = userWriteOnlyRepository;
    _unitOfWork = unitOfWork;
    _accessTokenGenerator = accessTokenGenerator;
  }

  public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
  {
    await Validate(request: request);

    var user = _mapper.Map<Domain.Entities.User>(source: request);
    user.Password = _passwordEncripter.Encrypt(password: request.Password);

    await _userWriteOnlyRepository.Add(user: user);
    await _unitOfWork.Commit();

    return new ResponseRegisteredUserJson
    {
      Name = user.Name,
      Email = user.Email,
      Token = _accessTokenGenerator.Generate(user: user)
    };
  }

  private async Task Validate(RequestRegisterUserJson request)
  {
    var result = new RegisterUserValidator().Validate(instance: request);
    bool emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(email: request.Email);

    if (emailExist)
    {
      result.Errors.Add(item: new ValidationFailure(propertyName: String.Empty, errorMessage: "Email already exists."));
    }

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(selector: x => x.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
