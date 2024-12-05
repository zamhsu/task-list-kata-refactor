using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.UseCase;

public class ExecuteUseCase
{
    private readonly ToDoList _toDoList;
    private readonly IConsole _console;
    private readonly IToDoListRepository _repository;

    public ExecuteUseCase(ToDoList toDoList, IConsole console, IToDoListRepository repository)
    {
        _toDoList = toDoList;
        _console = console;
        _repository = repository;
    }
    
    public void Execute(string commandLine)
    {
        var commandRest = commandLine.Split(" ".ToCharArray(), 2);
        var command = commandRest[0];
        switch (command)
        {
            case "show":
                new ShowUseCase(_toDoList, _console).Show();
                break;
            case "add":
                new AddUseCase(_toDoList, _console, _repository).Add(commandRest[1]);
                break;
            case "check":
                Check(commandRest[1]);
                break;
            case "uncheck":
                Uncheck(commandRest[1]);
                break;
            case "help":
                new HelpUseCase(_console).Help();
                break;
            default:
                new ErrorUseCase(_console).Error(command);
                break;
        }
    }
    
    private void Check(string idString)
    {
        new SetDoneUseCase(_toDoList, _console).SetDone(idString, true);
    }

    private void Uncheck(string idString)
    {
        new SetDoneUseCase(_toDoList, _console).SetDone(idString, false);
    }
}