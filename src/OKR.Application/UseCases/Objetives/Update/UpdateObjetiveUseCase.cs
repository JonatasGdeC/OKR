using AutoMapper;
using OKR.Application.UseCases.Objetives.Register;
using OKR.Communication.Requests;
using OKR.Communication.Response;
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

  public async Task Execute(Guid id, RequestObjectiveJson request)
  {
    Validate(request);

    Objective? objetive = await _repository.GetById(id);

    if (objetive == null)
    {
      //Error
    }

    Objective result = _mapper.Map(request, objetive);
    await _repository.Update(result);
    await _unitOfWork.Commit();
  }

  private void Validate(RequestObjectiveJson request)
  {
    var validator = new ObjectiveValidator();
    var result = validator.Validate(request);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
