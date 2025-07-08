using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;
using OKR.Domain.Secury.Tokens;
using OKR.Domain.Services.LoggedUser;
using OKR.Infrastructure.DataAccess;

namespace OKR.Infrastructure.Services.LoggedUser;

internal class LoggedUser : ILoggedUser
{
  private readonly OkrDbContext _context;
  private readonly ITokenProvider _tokenProvider;

  public LoggedUser(OkrDbContext context, ITokenProvider tokenProvider)
  {
    _context = context;
    _tokenProvider = tokenProvider;
  }

  public async Task<User> Get()
  {
    string token = _tokenProvider.TokenOnRequest();
    var tokenHandler = new JwtSecurityTokenHandler();
    var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
    var identifiear = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sid).Value;

    return await _context.Users.AsNoTracking().FirstAsync(user => user.Id == Guid.Parse(identifiear));
  }
}
