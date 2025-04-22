using Microsoft.AspNetCore.Mvc;
using OKR.Communication.Requests;

namespace OKR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuidelineController : ControllerBase
{
  [HttpPost]
  public IActionResult Register([FromBody] RequestRegisterGuideline request)
  {
    return Ok();
  }
}
