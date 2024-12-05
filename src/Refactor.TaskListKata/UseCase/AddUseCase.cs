using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.In.Project.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.Add;
using Refactor.TaskListKata.UseCase.Port.Out;
using Refactor.TaskListKata.UseCase.Service;

namespace Refactor.TaskListKata.UseCase;

public class AddUseCase
{
    private readonly ToDoList _toDoList;
    private readonly IConsole _console;
    private readonly IToDoListRepository _repository;

    public AddUseCase(ToDoList toDoList, IConsole console, IToDoListRepository repository)
    {
        _toDoList = toDoList;
        _console = console;
        _repository = repository;
    }
    
    public void Add(string commandLine)
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
}