using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Objetives.Delete;
using OKR.Application.UseCases.Objetives.GetAll;
using OKR.Application.UseCases.Objetives.GetByQuarterAndYear;
using OKR.Application.UseCases.Objetives.Register;
using OKR.Application.UseCases.Objetives.Update;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
[Authorize]
public class ObjectiveController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(type: typeof(ResponseObjectiveJson), statusCode: StatusCodes.Status201Created)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> RegisterObjective([FromServices] IRegisterObjectiveUseCase useCase, [FromBody] RequestRegisterObjectiveJson requestRegister)
  {
    var response = await useCase.Execute(requestRegister: requestRegister);
    return Created(uri: string.Empty, value: response);
  }

  [HttpGet]
  [ProducesResponseType(type: typeof(ResponseListObjectiveJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetAllObjectives([FromServices] IGetAllObjectiveUseCase useCase)
  {
    ResponseListObjectiveJson reponse = await useCase.Execute();
    if (reponse.ListObjectives.Count != 0)
    {
      return Ok(value: reponse);
    }

    return NoContent();
  }

  [HttpGet]
  [Route(template: "{quarter}/{year}")]
  [ProducesResponseType(type: typeof(ResponseObjectiveJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetObjetiveByQuarterAndYear([FromServices] IGetObjectiveByQuarterAndYear useCase, [FromRoute] int quarter, int year)
  {
    ResponseListObjectiveJson reponse = await useCase.Execute(quarter: quarter, year: year);
    if (reponse.ListObjectives.Count != 0)
    {
      return Ok(value: reponse);
    }

    return NoContent();
  }

  [HttpPut]
  [Route(template: "{id}")]
  [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> UpdateObjective([FromServices] IUpdateObjetiveUseCase useCase, [FromRoute] Guid id, [FromBody] RequestUpdateObjectiveJson requestRegister)
  {
    await useCase.Execute(id: id, requestRegister: requestRegister);
    return NoContent();
  }

  [HttpDelete]
  [Route(template: "{id}")]
  [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteObjective([FromServices] IDeleteObjectiveUseCase useCase, [FromRoute] Guid id)
  {
    await useCase.Execute(id: id);
    return NoContent();
  }
}
