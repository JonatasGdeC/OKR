using OKR.Domain.Secury;
using BC = BCrypt.Net.BCrypt;
namespace OKR.Infrastructure.Secury.Cryptography;

internal class BCrypt : IPasswordEncripter
{
  public string Encrypt(string password)
  {
    string passworHash = BC.HashPassword(password);
    return passworHash;
  }
}
