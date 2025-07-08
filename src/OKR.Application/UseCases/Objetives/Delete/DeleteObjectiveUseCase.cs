using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Objectives;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Objetives.Delete;

public class DeleteObjectiveUseCase : IDeleteObjectiveUseCase
{
  private readonly IObjetiveReadOnlyRepository _objectiveReadOnlyRepository;
  private readonly IObjectiveWriteOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILoggedUser _loggedUser;

  public DeleteObjectiveUseCase(IObjetiveReadOnlyRepository objectiveReadOnlyRepository, IObjectiveWriteOnlyRepository repository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
  {
    _objectiveReadOnlyRepository = objectiveReadOnlyRepository;
    _repository = repository;
    _unitOfWork = unitOfWork;
    _loggedUser = loggedUser;
  }

  public async Task Execute(Guid id)
  {
    var loggedUser = await _loggedUser.Get();
    ObjectiveEntity? objective = await _objectiveReadOnlyRepository.GetById(loggedUser, id);

    if (objective == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.OBJECTIVE_NOT_FOUND);
      return;
    }

    await _repository.Delete(id: id);
    await _unitOfWork.Commit();
  }
}
