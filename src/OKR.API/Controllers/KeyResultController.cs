using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.KeyResult.GetById;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class KeyResultController : ControllerBase
{
  [HttpGet]
  [Route(template: "{id}")]
  [ProducesResponseType(type: typeof(ResponseListKeyResultJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetKeyResultByObjectiveId([FromServices] IGetKeyResultByIdUseCase useCase, [FromRoute] Guid id)
  {
    var response = await useCase.Execute(id: id);
    return Ok(value: response);
  }
}
