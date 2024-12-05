using Refactor.TaskListKata.Adapter.Repository;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata;

public sealed class TaskList
{
    private const string QUIT = "quit";
    public const string DEFAULT_TO_DO_LIST_ID = "001";

    private readonly ToDoList toDoList = new ToDoList(new ToDoListId(DEFAULT_TO_DO_LIST_ID));
    private readonly IConsole console;
    private readonly IToDoListRepository repository;
    
    public static void Main(string[] args)
    {
        new TaskList(new RealConsole()).Run();
    }

    public TaskList(IConsole console)
    {
        this.console = console;
        repository = new ToDoListInMemoryRepository();

        if (repository.FindById(new ToDoListId(DEFAULT_TO_DO_LIST_ID)) is null)
        {
            repository.Save(toDoList);
        }
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

            new ExecuteUseCase(toDoList, console, repository).Execute(command);
        }
    }
}