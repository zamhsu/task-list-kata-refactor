using System.Collections.Generic;
using System.Linq;

namespace Refactor.TaskListKata.Entity;

public class Project
{
    private ProjectName _projectName;
    private readonly List<Task> _tasks;

    public Project(ProjectName projectName)
    {
        _projectName = projectName;
        _tasks = new List<Task>();
    }

    public Project(ProjectName projectName, List<Task> tasks) : this(projectName)
    {
        _tasks.AddRange(tasks);
    }

    public ProjectName GetName()
    {
        return _projectName;
    }

    public List<Task> GetTasks()
    {
        return _tasks;
    }

    public bool ContainTask(TaskId taskId)
    {
        return _tasks.Any(task => task.GetId().Equals(taskId));
    }

    public void SetTaskDone(TaskId taskId, bool isDone)
    {
        _tasks.FirstOrDefault(task => task.GetId().Equals(taskId))?.SetDone(isDone);
    }

    public void AddTask(Task task)
    {
        _tasks.Add(task);
    }
}