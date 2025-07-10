using OKR.Domain.Secury;
using OKR.Domain.Secury.Cryptography;
using BC = BCrypt.Net.BCrypt;
namespace OKR.Infrastructure.Secury.Cryptography;

internal class BCrypt : IPasswordEncripter
{
  public string Encrypt(string password) => BC.HashPassword(inputKey: password);
  public bool Verify(string password, string hashedPassword) => BC.Verify(text: password, hash: hashedPassword);
}
