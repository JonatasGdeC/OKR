using OKR.Domain.Entities;

namespace OKR.Domain.Services.LoggedUser;

public interface ILoggedUser
{
  Task<User> Get();
}
