using Refactor.TaskListKata.SeedWork;

namespace Refactor.TaskListKata.Entity;

public class ToDoListId : ValueObject<ToDoListId>
{
    public string Value { get; }

    public ToDoListId(string value)
    {
        Value = value;
    }
    
    public static implicit operator ToDoListId(string value) => new ToDoListId(value);
}