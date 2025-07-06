using OKR.Domain.Secury;
using BC = BCrypt.Net.BCrypt;
namespace OKR.Infrastructure.Secury.Cryptography;

internal class BCrypt : IPasswordEncripter
{
  public string Encrypt(string password) => BC.HashPassword(password);
  public bool Verify(string password, string hashedPassword) => BC.Verify(password, hashedPassword);
}
