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
    Validate(requestRegister);
    var entity = _mapper.Map<Objective>(requestRegister);
    await _repository.Add(entity);
    await _unitOfWork.Commit();
    return _mapper.Map<ResponseObjectiveJson>(entity);
  }

  private void Validate(RequestRegisterObjectiveJson requestRegister)
  {
    var validator = new RegisterObjectiveValidator();
    var result = validator.Validate(requestRegister);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
