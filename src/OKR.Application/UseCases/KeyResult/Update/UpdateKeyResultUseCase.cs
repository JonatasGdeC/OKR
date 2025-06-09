using AutoMapper;
using OKR.Communication.Requests;
using OKR.Domain.Entities;
using OKR.Domain.Repositories;
using OKR.Domain.Repositories.KeyResults;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.Application.UseCases.KeyResult.Update;

public class UpdateKeyResultUseCase : IUpdateKeyResultUseCase
{
  private readonly IKeyResultUpdateOnlyRepository _repository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public UpdateKeyResultUseCase(IKeyResultUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Execute(Guid id, RequestRegisterKeyResultJson request)
  {
    Validate(requestRegister: request);

    KeyResultEntity? keyResult = await _repository.GetById(id: id);

    if (keyResult == null)
    {
      throw new NotFoundException(message: ResourceErrorMessage.KEY_RESULT_NOT_FOUND);
    }

    KeyResultEntity result = _mapper.Map(source: request, destination: keyResult);

    await _repository.Update(keyResult: result);
    await _unitOfWork.Commit();
  }

  private void Validate(RequestRegisterKeyResultJson requestRegister)
  {
    var validator = new RegisterKeyResultValidator();
    var result = validator.Validate(instance: requestRegister);

    if (!result.IsValid)
    {
      List<string> errorMessages = result.Errors.Select(selector: f => f.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorsMessages: errorMessages);
    }
  }
}
