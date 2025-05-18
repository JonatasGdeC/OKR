namespace OKR.Domain.Repositories;

public interface IUnitOfWork
{
  Task Commit();
}
