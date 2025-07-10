using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.KeyResults;
using OKR.Domain.Services.LoggedUser;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.KeyResult.Delete;

public class DeleteKeyResultUseCase : IDeleteKeyResultUseCase
{
  private readonly IKeyResultWriteOnlyRepository _keyResultWriteOnly;
  private readonly IKeyResultReadOnlyRepository _keyResultReadOnly;
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILoggedUser _loggedUser;

  public DeleteKeyResultUseCase(IKeyResultWriteOnlyRepository keyResultWriteOnly, IKeyResultReadOnlyRepository keyResultReadOnly, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
  {
    _keyResultWriteOnly = keyResultWriteOnly;
    _keyResultReadOnly = keyResultReadOnly;
    _unitOfWork = unitOfWork;
    _loggedUser = loggedUser;
  }

  public async Task Execute(Guid id)
  {
    var loggedUser = await _loggedUser.Get();
    KeyResultEntity? keyResult = await _keyResultReadOnly.GetById(loggedUser: loggedUser, id: id);

    if (keyResult == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.KEY_RESULT_NOT_FOUND);
      return;
    }

    await _keyResultWriteOnly.Delete(id: id);
    await _unitOfWork.Commit();
  }
}
