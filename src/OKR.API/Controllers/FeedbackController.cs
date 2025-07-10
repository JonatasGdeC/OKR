using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OKR.Application.UseCases.Feedback.Delete;
using OKR.Application.UseCases.Feedback.GetFeedbacksByAction;
using OKR.Application.UseCases.Feedback.GetFeedbacksByDateRange;
using OKR.Application.UseCases.Feedback.Register;
using OKR.Application.UseCases.Feedback.Update;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.API.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
[Authorize]
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

  [HttpGet]
  [Route(template: "{actionId}")]
  [ProducesResponseType(type: typeof(ResponseListFeedbacksJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetFeedbacksByActionId([FromServices] IGetFeedbacksByActionIdUseCase useCase, [FromRoute] Guid actionId)
  {
    var response = await useCase.Execute(actionId: actionId);
    return Ok(value: response);
  }

  [HttpGet]
  [Route(template: "{dateStart}/{dateEnd}")]
  [ProducesResponseType(type: typeof(ResponseListFeedbacksJson), statusCode: StatusCodes.Status200OK)]
  [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetFeedbacksByDateRange([FromServices] IGetFeedbacksByDateRangeUseCase useCase, [FromRoute] DateTime dateStart, [FromRoute] DateTime dateEnd)
  {
    var response = await useCase.Execute(dateStart: dateStart, dateEnd: dateEnd);
    return Ok(value: response);
  }

  [HttpPut]
  [Route(template: "{feedbackId}")]
  [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> UpdateFeedback([FromServices] IUpdateFeedbackUseCase useCase, [FromRoute] Guid feedbackId, RequestRegisterFeedbackJson request)
  {
    await useCase.Execute(feedbackId: feedbackId, request: request);
    return NoContent();
  }

  [HttpDelete]
  [Route(template: "{feedbackId}")]
  [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
  [ProducesResponseType(type: typeof(ResponseErrorJson), statusCode: StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteKeyResult([FromServices] IDeleteFeedbackUseCase useCase, [FromRoute] Guid feedbackId)
  {
    await useCase.Execute(feedbackId: feedbackId);
    return NoContent();
  }
}
