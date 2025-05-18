using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Objetives.GetAll;
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

  [HttpGet]
  [ProducesResponseType(typeof(ResponseListObjectiveJson), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetAll([FromServices] IGetAllExpenseUseCase useCase)
  {
    ResponseListObjectiveJson reponse = await useCase.Execute();
    if (reponse.ListObjectives.Count != 0)
    {
      return Ok(reponse);
    }

    return NoContent();
  }
}
