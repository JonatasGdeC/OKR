using Microsoft.EntityFrameworkCore;
using OKR.Domain.Entities;
using OKR.Domain.Repositories.User;

namespace OKR.Infrastructure.DataAccess.Repositories;

internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
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

  public async Task<User?> GetUserByEmail(string email)
  {
    return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
  }

  public async Task Add(User user)
  {
    await _context.Users.AddAsync(user);
  }
}
