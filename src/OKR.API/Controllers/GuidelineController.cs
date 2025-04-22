using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Guidelines.Register;
using OKR.Communication.Requests;

namespace OKR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuidelineController : ControllerBase
{
  [HttpPost]
  public IActionResult Register([FromBody] RequestRegisterGuideline request)
  {
    var useCase = new RegisterGuidelineUseCase().Execute(request);
    return Created(string.Empty, useCase);
  }
}
