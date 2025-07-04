namespace OKR.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
  Task Add(Entities.User user);
}
