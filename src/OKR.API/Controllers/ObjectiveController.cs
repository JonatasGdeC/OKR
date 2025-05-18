using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Objetives.Register;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ObjectiveController : ControllerBase
{

  [HttpPost]
  [ProducesResponseType(typeof(ResponseObjectiveJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register([FromServices] IRegisterObjectiveUseCase useCase, [FromBody] RequestObjectiveJson request)
  {
    var response = await useCase.Execute(request);
    return Created(string.Empty, response);
  }
}
