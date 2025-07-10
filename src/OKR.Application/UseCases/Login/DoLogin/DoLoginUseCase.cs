using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Repositories.User;
using OKR.Domain.Secury;
using OKR.Domain.Secury.Cryptography;
using OKR.Domain.Tokens;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
  private readonly IUserReadOnlyRepository _userRepository;
  private readonly IPasswordEncripter _passwordEncripter;
  private readonly IAccessTokenGenerator _accessTokenGenerator;

  public DoLoginUseCase(IUserReadOnlyRepository userRepository, IPasswordEncripter passwordEncripter,  IAccessTokenGenerator accessTokenGenerator)
  {
    _userRepository = userRepository;
    _passwordEncripter = passwordEncripter;
    _accessTokenGenerator = accessTokenGenerator;
  }

  public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
  {
    var user = await _userRepository.GetUserByEmail(email: request.Email);
    if (user == null)
    {
      throw new InvalidLoginException();
    }

    bool passwordMetch = _passwordEncripter.Verify(password: request.Password, hashedPassword: user.Password);
    if (!passwordMetch)
    {
      throw new InvalidLoginException();
    }

    return new ResponseRegisteredUserJson
    {
      Name = user.Name,
      Email = user.Email,
      Token = _accessTokenGenerator.Generate(user: user)
    };
  }
}
