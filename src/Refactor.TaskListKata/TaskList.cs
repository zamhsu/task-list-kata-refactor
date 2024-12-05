using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase;

namespace Refactor.TaskListKata;

public sealed class TaskList
{
    private const string QUIT = "quit";
    private const string DEFAULT_TO_DO_LIST_ID = "001";

    private readonly ToDoList toDoList = new ToDoList(new ToDoListId(DEFAULT_TO_DO_LIST_ID));
    private readonly IConsole console;
    
    public static void Main(string[] args)
    {
        new TaskList(new RealConsole()).Run();
    }

    public TaskList(IConsole console)
    {
        this.console = console;
    }

    public void Run()
    {
        while (true)
        {
            console.Write("> ");
            var command = console.ReadLine();
            if (command == QUIT)
            {
                break;
            }

            new ExecuteUseCase(toDoList, console).Execute(command);
        }
    }
}