using System.Collections.Generic;

namespace Refactor.TaskListKata.UseCase.Port;

public class ProjectDto
{
    public string Name { get; set; }
    
    public List<TaskDto> TaskDtos { get; set; }
}