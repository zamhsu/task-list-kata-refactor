using Refactor.TaskListKata.SeedWork;

namespace Refactor.TaskListKata.Entity;

public class ProjectName : ValueObject<ProjectName>
{
    private string Value { get; }

    public ProjectName(string value)
    {
        Value = value;
    }
    
    public override string ToString() => Value;
}
