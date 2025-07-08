using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.User.Register;
using OKR.Communication.Requests;
using OKR.Communication.Response;
using OKR.Domain.Enums;

namespace OKR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class UserController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> RegisterUser([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
  {
    var response = await useCase.Execute(request);
    return Created(string.Empty, response);
  }
}
