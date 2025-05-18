using AutoMapper;
using OKR.Communication.Requests;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Objectives;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Objetives.Update;

public class UpdateObjetiveUseCase : IUpdateObjetiveUseCase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly IObjetiveUpdateOnlyRepository _repository;

  public UpdateObjetiveUseCase(IUnitOfWork unitOfWork, IMapper mapper, IObjetiveUpdateOnlyRepository repository)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _repository = repository;
  }

  public async Task Execute(Guid id, RequestUpdateObjectiveJson requestRegister)
  {
    Validate(requestRegister: requestRegister);

    Objective? objetive = await _repository.GetById(id: id);

    if (objetive == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.OBJECTIVE_NOT_FOUND);
    }

    Objective result = _mapper.Map(source: requestRegister, destination: objetive);
    await _repository.Update(objective: result);
    await _unitOfWork.Commit();
  }

  private void Validate(RequestUpdateObjectiveJson requestRegister)
  {
    var validator = new UpdateObjectiveValidator();
    var result = validator.Validate(instance: requestRegister);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
