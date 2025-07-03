namespace OKR.Domain.Secury;

public interface IPasswordEncripter
{
  string Encrypt(string password);
}
