using Refactor.TaskListKata.Entity;

namespace Refactor.TaskListKata.UseCase;

public class AddUseCase
{
    private readonly ToDoList _toDoList;
    private readonly IConsole _console;

    public AddUseCase(ToDoList toDoList, IConsole console)
    {
        _toDoList = toDoList;
        _console = console;
    }
    
    public void Add(string commandLine)
    {
        var subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
        var subcommand = subcommandRest[0];
        if (subcommand == "project")
        {
            AddProject(new ProjectName(subcommandRest[1]));
        }
        else if (subcommand == "task")
        {
            var projectTask = subcommandRest[1].Split(" ".ToCharArray(), 2);
            AddTask(new ProjectName(projectTask[0]), projectTask[1]);
        }
    }

    private void AddProject(ProjectName projectName)
    {
        _toDoList.AddProject(projectName);
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