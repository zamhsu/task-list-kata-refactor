namespace Refactor.TaskListKata.UseCase.Port.In.Task.Add;

public class AddTaskInput
{
    public string ToDoListId { get; set; }
    
    public string ProjectName { get; set; }
    
    public string Description { get; set; }
    
    public bool Done { get; set; }
}