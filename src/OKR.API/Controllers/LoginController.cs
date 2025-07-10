using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Login;
using OKR.Application.UseCases.Login.DoLogin;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(type: typeof(ResponseRegisteredUserJson),statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status401Unauthorized)]
  public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
  {
    var response = await useCase.Execute(request: request);
    return Ok(value: response);
  }
}
