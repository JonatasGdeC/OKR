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

  public async Task<ResponseObjectiveJson> Execute(RequestObjectiveJson request)
  {
    Validate(request);
    var entity = _mapper.Map<Objective>(request);
    await _repository.Add(entity);
    await _unitOfWork.Commit();
    return _mapper.Map<ResponseObjectiveJson>(entity);
  }

  private void Validate(RequestObjectiveJson request)
  {
    var validator = new RegisterObjectiveValidator();
    var result = validator.Validate(request);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
