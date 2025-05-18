using Microsoft.Extensions.DependencyInjection;
using OKR.Application.AutoMapper;
using OKR.Application.UseCases.Objetives.Delete;
using OKR.Application.UseCases.Objetives.GetAll;
using OKR.Application.UseCases.Objetives.Register;
using OKR.Application.UseCases.Objetives.Update;

namespace OKR.Application;

public static class DependencyInjectionExtension
{
  public static void AddApplication(this IServiceCollection services)
  {
    AddAutoMapper(services);
    AddUseCase(services);
  }

  private static void AddAutoMapper(this IServiceCollection services)
  {
    services.AddAutoMapper(typeof(AutoMapping));
  }

  private static void AddUseCase(this IServiceCollection services)
  {
    services.AddScoped<IRegisterObjectiveUseCase, RegisterObjectiveUseCase>();
    services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
    services.AddScoped<IUpdateObjetiveUseCase, UpdateObjetiveUseCase>();
    services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
  }
}
