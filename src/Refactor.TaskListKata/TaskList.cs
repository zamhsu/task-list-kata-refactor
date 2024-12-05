using Refactor.TaskListKata.Adapter.Repository;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata;

public sealed class TaskList
{
    private const string QUIT = "quit";
    public const string DEFAULT_TO_DO_LIST_ID = "001";

    private readonly ToDoList _toDoList = new ToDoList(new ToDoListId(DEFAULT_TO_DO_LIST_ID));
    private readonly IConsole _console;
    private readonly IToDoListRepository _repository;
    
    public static void Main(string[] args)
    {
        new TaskList(new RealConsole()).Run();
    }

    public TaskList(IConsole console)
    {
        _console = console;
        _repository = new ToDoListInMemoryRepository();

        if (_repository.FindById(new ToDoListId(DEFAULT_TO_DO_LIST_ID)) is null)
        {
            _repository.Save(_toDoList);
        }
    }

    public void Run()
    {
        while (true)
        {
            _console.Write("> ");
            var command = _console.ReadLine();
            if (command == QUIT)
            {
                break;
            }

            new ExecuteUseCase(_toDoList, _console, _repository).Execute(command);
        }
    }
}