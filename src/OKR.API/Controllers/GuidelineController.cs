using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Guidelines.Delete;
using OKR.Application.UseCases.Guidelines.GetAll;
using OKR.Application.UseCases.Guidelines.Register;
using OKR.Communication.Requests;
using OKR.Communication.Response.Guideline;

namespace OKR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuidelineController : ControllerBase
{
  //Simulation
  private static readonly List<ResponseRegisterGuidelineJson> ListGuidelines = new List<ResponseRegisterGuidelineJson>();

  [HttpPost]
  public IActionResult Register([FromBody] RequestRegisterGuideline request)
  {
    var useCase = new RegisterGuidelineUseCase().Execute(request);
    ListGuidelines.Add(useCase);
    return Created(string.Empty, useCase);
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    var useCase = new GetAllGuidelineUseCase().Execute(listSimulation: ListGuidelines);
    if (useCase.Guidelines.Any())
    {
      return Ok(useCase);
    }

    return NoContent();
  }

  [HttpPut]
  [Route("{id}")]
  public IActionResult Update([FromRoute] Guid id, [FromBody] RequestRegisterGuideline request)
  {
    return NoContent();
  }

  [HttpDelete]
  [Route("{id}")]
  public IActionResult Delete(Guid id)
  {
    var useCase = new DeleteGuidelineUseCase();
    useCase.Execute(id);
    ListGuidelines.RemoveAll(guideline => guideline.Id == id);
    return NoContent();
  }
}
