using Microsoft.EntityFrameworkCore;
using OKR.Domain.Repositories.User;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class UserRepository : IUserReadOnlyRepository
{
  private readonly OkrDbContext _context;

  public UserRepository(OkrDbContext context)
  {
    _context = context;
  }

  public async Task<bool> ExistActiveUserWithEmail(string email)
  {
    return await _context.Users.AnyAsync(user => user.Email == email);
  }
}
