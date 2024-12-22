using Refactor.TaskListKata.Adapter.Presenter;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.In.Project.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Show;
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
                IShowUseCase showUseCase = new ShowService(_repository, new ShowConsolePresenter(_console));
                var showInput = new ShowInput();
                showInput.ToDoListId = _toDoList.GetId().ToString();
                showUseCase.Execute(showInput);
                break;
            case "add":
                Add(commandRest[1]);
                break;
            case "check":
                SetDone(commandRest[1], true);
                break;
            case "uncheck":
                SetDone(commandRest[1], false);
                break;
            case "help":
                IHelpUseCase helpUseCase = new HelpService(new HelpConsolePresenter(_console));
                helpUseCase.Execute();
                break;
            default:
                IErrorUseCase errorUseCase = new ErrorService(_console);
                var errorInput = new ErrorInput();
                errorInput.Command = command;
                errorUseCase.Execute(errorInput);
                break;
        }
    }
    
    private void Add(string commandLine)
    {
        var subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
        var subcommand = subcommandRest[0];
        if (subcommand == "project")
        {
            IAddProjectUseCase addProjectUseCase = new AddProjectService(_repository);
            var addProjectInput = new AddProjectInput();
            addProjectInput.ToDoListId = TaskList.DEFAULT_TO_DO_LIST_ID;
            addProjectInput.ProjectName = subcommandRest[1];
            addProjectUseCase.Execute(addProjectInput);
        }
        else if (subcommand == "task")
        {
            var projectTask = subcommandRest[1].Split(" ".ToCharArray(), 2);
            
            IAddTaskUseCase addTaskUseCase = new AddTaskService(_repository, _console);
            var addTaskInput = new AddTaskInput();
            addTaskInput.ToDoListId = TaskList.DEFAULT_TO_DO_LIST_ID;
            addTaskInput.ProjectName = projectTask[0];
            addTaskInput.Description = projectTask[1];
            addTaskInput.Done = false;
            addTaskUseCase.Execute(addTaskInput);
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