using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Feedback.Register;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class FeedbackController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(type: typeof(ResponseFeedbackJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> RegisterFeedback([FromServices] IRegisterFeedbackUseCase useCase, RequestRegisterFeedbackJson request)
  {
    var response = await useCase.Execute(request: request);
    return Created(uri: string.Empty, value: response);
  }
}
