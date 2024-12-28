namespace Refactor.TaskListKata.UseCase.Port;

public class TaskDto
{
    public string Id { get; set; }
    
    public string Description { get; set; }
    
    public bool Done { get; set; }
}