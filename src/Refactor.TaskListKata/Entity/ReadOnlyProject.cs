using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactor.TaskListKata.Entity;

public class ReadOnlyProject : Project
{
    private Project _real;

    public ReadOnlyProject(Project real) : base(real.GetName(), real.GetTasks())
    {
        _real = real;
    }

    public override List<Task> GetTasks()
    {
        return _real.GetTasks().Select(task => new ReadOnlyTask(task) as Task).ToList();
    }

    public override void SetTaskDone(TaskId taskId, bool done)
    {
        throw new InvalidOperationException("Read only.");
    }

    public override void AddTask(Task task)
    {
        throw new InvalidOperationException("Read only.");
    }
}