using AutoMapper;
using OKR.Communication.Requests;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Objectives;
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
    Validate(requestRegister);

    Objective? objetive = await _repository.GetById(id);

    if (objetive == null)
    {
      //Error
    }

    Objective result = _mapper.Map(requestRegister, objetive);
    await _repository.Update(result);
    await _unitOfWork.Commit();
  }

  private void Validate(RequestUpdateObjectiveJson requestRegister)
  {
    var validator = new UpdateObjectiveValidator();
    var result = validator.Validate(requestRegister);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
