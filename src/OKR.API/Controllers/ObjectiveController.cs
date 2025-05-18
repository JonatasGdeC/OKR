using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Objetives.Delete;
using OKR.Application.UseCases.Objetives.GetAll;
using OKR.Application.UseCases.Objetives.Register;
using OKR.Application.UseCases.Objetives.Update;
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
  public async Task<IActionResult> RegisterObjective([FromServices] IRegisterObjectiveUseCase useCase, [FromBody] RequestRegisterObjectiveJson requestRegister)
  {
    var response = await useCase.Execute(requestRegister);
    return Created(string.Empty, response);
  }

  [HttpGet]
  [ProducesResponseType(typeof(ResponseListObjectiveJson), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetAllObjectives([FromServices] IGetAllExpenseUseCase useCase)
  {
    ResponseListObjectiveJson reponse = await useCase.Execute();
    if (reponse.ListObjectives.Count != 0)
    {
      return Ok(reponse);
    }

    return NoContent();
  }

  [HttpPut]
  [Route("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> UpdateObjective([FromServices] IUpdateObjetiveUseCase useCase, [FromRoute] Guid id, [FromBody] RequestUpdateObjectiveJson requestRegister)
  {
    await useCase.Execute(id, requestRegister);
    return NoContent();
  }

  [HttpDelete]
  [Route("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteObjective([FromServices] IDeleteExpenseUseCase useCase, [FromRoute] Guid id)
  {
    await useCase.Execute(id);
    return NoContent();
  }
}
