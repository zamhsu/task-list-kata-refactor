using Microsoft.Extensions.DependencyInjection;
using Refactor.TaskListKata.Adapter.Console;
using Refactor.TaskListKata.Adapter.Presenter;
using Refactor.TaskListKata.Adapter.Repository;
using Refactor.TaskListKata.IO.Console;
using Refactor.TaskListKata.UseCase.Port.In.Project.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Show;
using Refactor.TaskListKata.UseCase.Port.Out;
using Refactor.TaskListKata.UseCase.Service;

namespace Refactor.TaskListKata.IO.DependencyInjection.MainComponent;

public static class ToDoListServiceCollectionExtensions
{
    public static IServiceCollection AddInPortModule(this IServiceCollection services)
    {
        services.AddSingleton<IAddProjectUseCase, AddProjectService>();
        services.AddSingleton<IAddTaskUseCase, AddTaskService>();
        services.AddSingleton<ISetDoneUseCase, SetDoneService>();
        services.AddSingleton<IHelpUseCase, HelpService>();
        services.AddSingleton<IShowUseCase, ShowService>();
        services.AddSingleton<IErrorUseCase, ErrorService>();
        
        return services;
    }

    public static IServiceCollection AddOutPortModule(this IServiceCollection services)
    {
        services.AddSingleton<IToDoListRepository, ToDoListInMemoryRepository>();

        services.AddSingleton<IHelpPresenter, HelpConsolePresenter>();
        services.AddSingleton<IShowPresenter, ShowConsolePresenter>();
        services.AddSingleton<IErrorPresenter, ErrorConsolePresenter>();
        services.AddSingleton<ISystemErrorPresenter, SystemErrorConsolePresenter>();

        return services;
    }

    public static IServiceCollection AddAdapterModule(this IServiceCollection services)
    {
        services.AddSingleton<IConsole, RealConsole>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<Application>();
        
        return services;
    }
}