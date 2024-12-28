namespace Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;

public class SetDoneInput
{
    public string ToDoListId { get; set; }
    
    public string TaskId { get; set; }
    
    public bool Done { get; set; }
}