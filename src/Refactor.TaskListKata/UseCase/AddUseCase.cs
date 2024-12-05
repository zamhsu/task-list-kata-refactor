using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.In.Project.Add;
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
            AddTask(new ProjectName(projectTask[0]), projectTask[1]);
        }
    }

    private void AddTask(ProjectName projectName, string description)
    {
        var projectTasks = _toDoList.GetTasks(projectName);

        if (projectTasks is null)
        {
            _console.WriteLine("Could not find a project with the name \"{0}\".", projectName);
            return;
        }

        _toDoList.AddTask(projectName, description, false);
    }
}