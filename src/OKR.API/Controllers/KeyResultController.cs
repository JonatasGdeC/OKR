using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.KeyResult.GetById;
using OKR.Application.UseCases.KeyResult.Register;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class KeyResultController : ControllerBase
{

  [HttpPost]
  [ProducesResponseType(type: typeof(ResponseKeyResultJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> RegisterKeyResult([FromServices] IRegisterKeyResultUseCase useCase, RequestRegisterKeyResultJson request)
  {
    var response = await useCase.Execute(requestRegister: request);
    return Created(uri: string.Empty, response);
  }

  [HttpGet]
  [Route(template: "{objectiveId}")]
  [ProducesResponseType(type: typeof(ResponseListKeyResultJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetKeyResultByObjectiveId([FromServices] IGetKeyResultByIdUseCase useCase, [FromRoute] Guid objectiveId)
  {
    var response = await useCase.Execute(id: objectiveId);
    return Ok(value: response);
  }
}
