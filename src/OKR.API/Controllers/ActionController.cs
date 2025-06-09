using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Action.GetActionByKeyResultId;
using OKR.Application.UseCases.Action.Register;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class ActionController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(type: typeof(ResponseActionJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> RegisterAction([FromServices] IRegisterActionUseCase useCase, RequestRegisterActionJson request)
  {
    var response = await useCase.Execute(requestRegister: request);
    return Created(uri: string.Empty, value: response);
  }

  [HttpGet]
  [Route(template: "{keyResultId}")]
  [ProducesResponseType(type: typeof(ResponseListActionJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetActionsByKeyResultId([FromServices] IGetActionsByKeyResultIdUseCase useCase, [FromRoute] Guid keyResultId)
  {
    var response = await useCase.Execute(keyResultId: keyResultId);
    return Ok(value: response);
  }
}
