using System;

namespace Refactor.TaskListKata.Entity;

public class ReadOnlyTask : Task
{
    public ReadOnlyTask(Task real) : base(real.GetId(), real.GetDescription(), real.IsDone())
    {
        
    }

    public override void SetDone(bool done)
    {
        throw new InvalidOperationException("Read only.");
    }
}