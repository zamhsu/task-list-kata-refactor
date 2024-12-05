namespace Refactor.TaskListKata.UseCase;

public class ErrorUseCase
{
    private readonly IConsole _console;

    public ErrorUseCase(IConsole console)
    {
        _console = console;
    }
    
    public void Error(string command)
    {
        _console.WriteLine("I don't know what the command \"{0}\" is.", command);
    }
}