using OKR.Communication.Response.Guideline;

namespace OKR.Application.UseCases.Guidelines.GetAll;

public class GetAllGuidelineUseCase
{
  public ResponseAllGuidelineJson Execute(List<ResponseRegisterGuidelineJson> listSimulation)
  {
    ResponseAllGuidelineJson response = new ResponseAllGuidelineJson();
    response.Guidelines = listSimulation;

    return response;
  }
}
