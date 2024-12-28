using Refactor.TaskListKata.SeedWork;

namespace Refactor.TaskListKata.Entity;

public class TaskId : ValueObject<TaskId>
{
    public string Value { get; }

    public TaskId(long value)
    {
        Value = value.ToString();
    }

    public TaskId(string value)
    {
        Value = value;
    }
    
    public override string ToString() => Value;
}