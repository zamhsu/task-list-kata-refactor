using Refactor.TaskListKata.IO.Standard;
using Refactor.TaskListKata.UseCase.Port.In.Project.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Show;

namespace Refactor.TaskListKata.Adapter.Controller;

public class ToDoListConsoleController
{
    private readonly IShowUseCase _showUseCase;
    private readonly IAddProjectUseCase _addProjectUseCase;
    private readonly IAddTaskUseCase _addTaskUseCase;
    private readonly ISetDoneUseCase _setDoneUseCase;
    private readonly IHelpUseCase _helpUseCase;
    private readonly IErrorUseCase _errorUseCase;
    
    public ToDoListConsoleController(IShowUseCase showUseCase, 
                                     IAddProjectUseCase addProjectUseCase, 
                                     IAddTaskUseCase addTaskUseCase, 
                                     ISetDoneUseCase setDoneUseCase, 
                                     IHelpUseCase helpUseCase, 
                                     IErrorUseCase errorUseCase)
    {
        _showUseCase = showUseCase;
        _addProjectUseCase = addProjectUseCase;
        _addTaskUseCase = addTaskUseCase;
        _setDoneUseCase = setDoneUseCase;
        _helpUseCase = helpUseCase;
        _errorUseCase = errorUseCase;
    }
    
    public void Execute(string commandLine)
    {
        var commandRest = commandLine.Split(" ".ToCharArray(), 2);
        var command = commandRest[0];
        switch (command)
        {
            case "show":
                var showInput = new ShowInput();
                showInput.ToDoListId = ToDoListApp.DEFAULT_TO_DO_LIST_ID;
                _showUseCase.Execute(showInput);
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
                _helpUseCase.Execute();
                break;
            default:
                var errorInput = new ErrorInput();
                errorInput.Command = commandLine;
                _errorUseCase.Execute(errorInput);
                break;
        }
    }
    
    private void Add(string commandLine)
    {
        var subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
        var subcommand = subcommandRest[0];
        if (subcommand == "project")
        {
            var addProjectInput = new AddProjectInput();
            addProjectInput.ToDoListId = ToDoListApp.DEFAULT_TO_DO_LIST_ID;
            addProjectInput.ProjectName = subcommandRest[1];
            _addProjectUseCase.Execute(addProjectInput);
        }
        else if (subcommand == "task")
        {
            var projectTask = subcommandRest[1].Split(" ".ToCharArray(), 2);
            
            var addTaskInput = new AddTaskInput();
            addTaskInput.ToDoListId = ToDoListApp.DEFAULT_TO_DO_LIST_ID;
            addTaskInput.ProjectName = projectTask[0];
            addTaskInput.Description = projectTask[1];
            addTaskInput.Done = false;
            _addTaskUseCase.Execute(addTaskInput);
        }
    }

    private void SetDone(string taskId, bool done)
    {
        var setDoneInput = new SetDoneInput();
        setDoneInput.ToDoListId = ToDoListApp.DEFAULT_TO_DO_LIST_ID;
        setDoneInput.TaskId = taskId;
        setDoneInput.Done = done;
        _setDoneUseCase.Execute(setDoneInput);
    }
}