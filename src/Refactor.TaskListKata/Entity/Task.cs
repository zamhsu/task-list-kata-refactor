namespace Refactor.TaskListKata.Entity;

public class Task
{
    private readonly TaskId _id;
    private readonly string _description;
    private bool _done;

    public Task(TaskId id, string description, bool done)
    {
        _id = id;
        _description = description;
        _done = done;
    }

    public TaskId GetId()
    {
        return _id;
    }

    public string GetDescription()
    {
        return _description;
    }

    public bool IsDone()
    {
        return _done;
    }

    public void SetDone(bool done)
    {
        _done = done;
    }
}