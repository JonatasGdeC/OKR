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
      new Claim(type: ClaimTypes.Sid, value: user.Id.ToString()),
      new Claim(type: ClaimTypes.Name, value: user.Name),
      new Claim(type: ClaimTypes.Role, value: user.Role)
    };

    var tokenDescription = new SecurityTokenDescriptor
    {
      Expires = DateTime.UtcNow.AddMinutes(value: _expirationTimeMinutes),
      SigningCredentials = new SigningCredentials(key: SecuryKey(), algorithm: SecurityAlgorithms.HmacSha256),
      Subject = new ClaimsIdentity(claims: claims)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor: tokenDescription);
    return tokenHandler.WriteToken(token: token);
  }

  private SymmetricSecurityKey SecuryKey()
  {
    var key = Encoding.UTF8.GetBytes(s: _signingKey);

    return new SymmetricSecurityKey(key: key);
  }
}
