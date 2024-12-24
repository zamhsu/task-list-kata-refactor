using Refactor.TaskListKata.Adapter.Console;
using Refactor.TaskListKata.UseCase.Port;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.Adapter.Presenter;

public class SystemErrorConsolePresenter : ISystemErrorPresenter
{
    private readonly IConsole _console;

    public SystemErrorConsolePresenter(IConsole console)
    {
        _console = console;
    }

    public void Present(SystemErrorDto systemErrorDto)
    {
        _console.WriteLine(systemErrorDto.Message);
    }
}