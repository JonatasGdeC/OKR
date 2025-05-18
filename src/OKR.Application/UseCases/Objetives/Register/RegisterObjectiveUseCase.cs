using AutoMapper;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.Objectives;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.Objetives.Register;

public class RegisterObjectiveUseCase : IRegisterObjectiveUseCase
{
  private readonly IObjectiveWriteOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public RegisterObjectiveUseCase(IObjectiveWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<ResponseObjectiveJson> Execute(RequestRegisterObjectiveJson requestRegister)
  {
    Validate(requestRegister: requestRegister);
    var entity = _mapper.Map<Objective>(source: requestRegister);
    await _repository.Add(objective: entity);
    await _unitOfWork.Commit();
    return _mapper.Map<ResponseObjectiveJson>(source: entity);
  }

  private void Validate(RequestRegisterObjectiveJson requestRegister)
  {
    var validator = new RegisterObjectiveValidator();
    var result = validator.Validate(instance: requestRegister);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
