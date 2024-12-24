using Refactor.TaskListKata.Adapter.Console;
using Refactor.TaskListKata.UseCase.Port;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.Adapter.Presenter;

public class ShowConsolePresenter : IShowPresenter
{
    private readonly IConsole _console;

    public ShowConsolePresenter(IConsole console)
    {
        _console = console;
    }
    
    public void Present(ToDoListDto toDoListDto)
    {
        foreach (var project in toDoListDto.ProjectDtos)
        {
            _console.WriteLine(project.Name);
            foreach (var task in project.TaskDtos)
            {
                _console.WriteLine("    [{0}] {1}: {2}", (task.Done ? 'x' : ' '), task.Id,
                    task.Description);
            }

            _console.WriteLine();
        }
    }
}