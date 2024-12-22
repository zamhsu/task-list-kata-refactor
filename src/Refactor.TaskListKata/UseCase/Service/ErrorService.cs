using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;

namespace Refactor.TaskListKata.UseCase.Service;

public class ErrorService : IErrorUseCase
{
    private readonly IConsole _console;

    public ErrorService(IConsole console)
    {
        _console = console;
    }

    public void Execute(ErrorInput input)
    {
        _console.WriteLine("I don't know what the command \"{0}\" is.", input.Command);
    }
}