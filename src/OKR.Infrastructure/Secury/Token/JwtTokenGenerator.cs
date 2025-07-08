using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OKR.Domain.Entities;
using OKR.Domain.Tokens;

namespace OKR.Infrastructure.Secury.Token;

public class JwtTokenGenerator : IAccessTokenGenerator
{
  private readonly uint _expirationTimeMinutes;
  private readonly string _signingKey;

  public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
  {
    _expirationTimeMinutes = expirationTimeMinutes;
    _signingKey = signingKey;
  }

  public string Generate(User user)
  {
    var claims = new List<Claim>()
    {
      new Claim(ClaimTypes.Sid, user.Id.ToString()),
      new Claim(ClaimTypes.Name, user.Name),
      new Claim(ClaimTypes.Role, user.Role)
    };

    var tokenDescription = new SecurityTokenDescriptor
    {
      Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
      SigningCredentials = new SigningCredentials(SecuryKey(), SecurityAlgorithms.HmacSha256),
      Subject = new ClaimsIdentity(claims)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescription);
    return tokenHandler.WriteToken(token);
  }

  private SymmetricSecurityKey SecuryKey()
  {
    var key = Encoding.UTF8.GetBytes(_signingKey);

    return new SymmetricSecurityKey(key);
  }
}
