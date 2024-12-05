using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;
using Refactor.TaskListKata.UseCase.Port.Out;
using Refactor.TaskListKata.UseCase.Service;

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
                SetDone(commandRest[1], true);
                break;
            case "uncheck":
                SetDone(commandRest[1], false);
                break;
            case "help":
                new HelpUseCase(_console).Help();
                break;
            default:
                new ErrorUseCase(_console).Error(command);
                break;
        }
    }

    private void SetDone(string taskId, bool done)
    {
        ISetDoneUseCase setDoneUseCase = new SetDoneService(_repository, _console);
        var setDoneInput = new SetDoneInput();
        setDoneInput.ToDoListId = TaskList.DEFAULT_TO_DO_LIST_ID;
        setDoneInput.TaskId = taskId;
        setDoneInput.Done = done;
        setDoneUseCase.Execute(setDoneInput);
    }
}