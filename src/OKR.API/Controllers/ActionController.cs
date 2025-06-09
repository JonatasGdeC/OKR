using Microsoft.AspNetCore.Mvc;
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
  public async Task<IActionResult> RegisterAction([FromServices] IRegisterActionUseCase useCase, RequestRegisterActionJson request)
  {
    var response = await useCase.Execute(requestRegister: request);
    return Created(uri: string.Empty, value: response);
  }
}
