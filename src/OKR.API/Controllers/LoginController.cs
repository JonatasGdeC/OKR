using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Login;
using OKR.Application.UseCases.Login.DoLogin;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(ResponseRegisteredUserJson),StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
  public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
  {
    var response = await useCase.Execute(request);
    return Ok(response);
  }
}
