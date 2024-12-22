using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.Adapter.Presenter;

public class HelpConsolePresenter : IHelperPresenter
{
    private readonly IConsole _console;

    public HelpConsolePresenter(IConsole console)
    {
        _console = console;
    }
    
    public void Present(HelpDto helpDto)
    {
        _console.WriteLine(helpDto.Heading);
        foreach (var command in helpDto.Commands)
        {
            _console.WriteLine($"  {command}");
        }
        _console.WriteLine();
    }
}