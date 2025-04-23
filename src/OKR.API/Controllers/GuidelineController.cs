using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Guidelines.Register;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuidelineController : ControllerBase
{
  //Simulation
  private List<ResponseRegisterGuidelineJson> _listGuidelines = new List<ResponseRegisterGuidelineJson>();

  [HttpPost]
  public IActionResult Register([FromBody] RequestRegisterGuideline request)
  {
    var useCase = new RegisterGuidelineUseCase().Execute(request);
    _listGuidelines.Add(useCase);
    return Created(string.Empty, useCase);
  }
}
