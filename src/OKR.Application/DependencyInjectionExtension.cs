using Microsoft.Extensions.DependencyInjection;
using OKR.Application.AutoMapper;
using OKR.Application.UseCases.Action.Delete;
using OKR.Application.UseCases.Action.GetActionByKeyResultId;
using OKR.Application.UseCases.Action.GetActionsByDateRange;
using OKR.Application.UseCases.Action.Register;
using OKR.Application.UseCases.Action.Update;
using OKR.Application.UseCases.Action.UpdateProgress;
using OKR.Application.UseCases.Feedback.Delete;
using OKR.Application.UseCases.Feedback.GetFeedbacksByAction;
using OKR.Application.UseCases.Feedback.GetFeedbacksByDateRange;
using OKR.Application.UseCases.Feedback.Register;
using OKR.Application.UseCases.Feedback.Update;
using OKR.Application.UseCases.KeyResult.Delete;
using OKR.Application.UseCases.KeyResult.GetById;
using OKR.Application.UseCases.KeyResult.Register;
using OKR.Application.UseCases.KeyResult.Update;
using OKR.Application.UseCases.Objetives.Delete;
using OKR.Application.UseCases.Objetives.GetAll;
using OKR.Application.UseCases.Objetives.GetByQuarterAndYear;
using OKR.Application.UseCases.Objetives.Register;
using OKR.Application.UseCases.Objetives.Update;
using OKR.Application.UseCases.User.Register;

namespace OKR.Application;

public static class DependencyInjectionExtension
{
  public static void AddApplication(this IServiceCollection services)
  {
    AddAutoMapper(services: services);
    AddUseCase(services: services);
  }

  private static void AddAutoMapper(this IServiceCollection services)
  {
    services.AddAutoMapper(profileAssemblyMarkerTypes: typeof(AutoMapping));
  }

  private static void AddUseCase(this IServiceCollection services)
  {
    services.AddScoped<IRegisterObjectiveUseCase, RegisterObjectiveUseCase>();
    services.AddScoped<IGetAllObjectiveUseCase, GetAllObjectiveUseCase>();
    services.AddScoped<IGetObjectiveByQuarterAndYear, GetObjectiveByQuarterAndYear>();
    services.AddScoped<IUpdateObjetiveUseCase, UpdateObjetiveUseCase>();
    services.AddScoped<IDeleteObjectiveUseCase, DeleteObjectiveUseCase>();

    services.AddScoped<IRegisterKeyResultUseCase, RegisterKeyResultUseCase>();
    services.AddScoped<IGetKeyResultByIdUseCase, GetKeyResultByObjectiveId>();
    services.AddScoped<IUpdateKeyResultUseCase, UpdateKeyResultUseCase>();
    services.AddScoped<IDeleteKeyResultUseCase, DeleteKeyResultUseCase>();

    services.AddScoped<IRegisterActionUseCase, RegisterActionUseCase>();
    services.AddScoped<IGetActionsByKeyResultIdUseCase, GetActionsByKeyResultIdUseCase>();
    services.AddScoped<IGetActionsByDateRangeUseCase, GetActionsByDateRangeUseCase>();
    services.AddScoped<IDeleteActionUseCase, DeleteActionUseCase>();
    services.AddScoped<IUpdateActionUseCase, UpdateActionUseCase>();
    services.AddScoped<IUpdateProgressActionUseCase, UpdateProgressActionUseCase>();

    services.AddScoped<IRegisterFeedbackUseCase, RegisterFeedbackUseCase>();
    services.AddScoped<IGetFeedbacksByActionIdUseCase, GetFeedbacksByActionIdUseCase>();
    services.AddScoped<IGetFeedbacksByDateRangeUseCase, GetFeedbacksByDateRangeUseCase>();
    services.AddScoped<IUpdateFeedbackUseCase, UpdateFeedbackUseCase>();
    services.AddScoped<IDeleteFeedbackUseCase, DeleteFeedbackUseCase>();

    services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
  }
}
