using OKR.Domain.Entities;

namespace OKR.Domain.Tokens;

public interface IAccessTokenGenerator
{
  string Generate(User user);
}
