using System.Collections.Generic;
using System.Linq;
using Refactor.TaskListKata.SeedWork;

namespace Refactor.TaskListKata.Entity;

public class ToDoList : AggregateRoot
{
    private readonly ToDoListId _id;
    private readonly List<Project> _projects;
    private long lastTaskId;

    public ToDoList(ToDoListId id)
    {
        _id = id;
        _projects = new List<Project>();
        lastTaskId = 0;
    }

    public List<Project> GetProjects()
    {
        return _projects.Select(project => new ReadOnlyProject(project) as Project).ToList();
    }

    public void AddProject(ProjectName name)
    {
        if (_projects.Any(project => project.GetName().Equals(project)))
        {
            return;
        }
        
        _projects.Add(new Project(name));
    }

    public List<Task> GetTasks(ProjectName projectName)
    {
        var result = _projects.FirstOrDefault(project => project.GetName().Equals(projectName));

        if (result is null)
        {
            return null;
        }
        
        return result.GetTasks().Select(task => new ReadOnlyTask(task) as Task).ToList();
    }

    public void AddTask(ProjectName name, string description, bool done)
    {
        var project = GetProject(name);

        if (project is null)
        {
            return;
        }
        
        project.AddTask(new Task(new TaskId(NextTaskId()), description, done));
    }

    public bool ContainTask(TaskId taskId)
    {
        return _projects.Any(project => project.ContainTask(taskId));
    }

    public Project GetProject(ProjectName projectName)
    {
        return _projects.FirstOrDefault(project => project.GetName().Equals(projectName));
    }

    public ToDoListId GetId()
    {
        return _id;
    }

    public void SetDone(TaskId taskId, bool done)
    {
        _projects.FirstOrDefault(project => project.ContainTask(taskId))?.SetTaskDone(taskId, done);
    }

    private long NextTaskId()
    {
        return ++lastTaskId;
    }
}