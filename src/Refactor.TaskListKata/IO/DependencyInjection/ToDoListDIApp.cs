using Microsoft.Extensions.DependencyInjection;
using Refactor.TaskListKata.IO.DependencyInjection.MainComponent;

namespace Refactor.TaskListKata.IO.DependencyInjection;

public sealed class ToDoListDIApp
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddInPortModule();
        services.AddOutPortModule();
        services.AddAdapterModule();
        services.AddApplication();
        
        var serviceProvider = services.BuildServiceProvider();
        
        serviceProvider.GetRequiredService<Application>().Run();
    }
}