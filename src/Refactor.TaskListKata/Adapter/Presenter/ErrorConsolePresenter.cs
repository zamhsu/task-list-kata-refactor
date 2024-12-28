using Refactor.TaskListKata.Adapter.Console;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.Adapter.Presenter;

public class ErrorConsolePresenter : IErrorPresenter
{
    private readonly IConsole _console;

    public ErrorConsolePresenter(IConsole console)
    {
        _console = console;
    }

    public void Present(ErrorDto errorDto)
    {
        _console.WriteLine(errorDto.Message);
    }
}