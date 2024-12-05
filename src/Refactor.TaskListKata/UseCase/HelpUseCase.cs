namespace Refactor.TaskListKata.UseCase;

public class HelpUseCase
{
    private readonly IConsole _console;

    public HelpUseCase(IConsole console)
    {
        _console = console;
    }
    
    public void Help()
    {
        _console.WriteLine("Commands:");
        _console.WriteLine("  show");
        _console.WriteLine("  add project <project name>");
        _console.WriteLine("  add task <project name> <task description>");
        _console.WriteLine("  check <task ID>");
        _console.WriteLine("  uncheck <task ID>");
        _console.WriteLine();
    }
}