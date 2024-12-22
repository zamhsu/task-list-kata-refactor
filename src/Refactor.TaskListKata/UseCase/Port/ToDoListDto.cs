using System.Collections.Generic;

namespace Refactor.TaskListKata.UseCase.Port;

public class ToDoListDto
{
    public string Id { get; set; }
    
    public List<ProjectDto> ProjectDtos { get; set; }

    public ToDoListDto()
    {
        ProjectDtos = new List<ProjectDto>();
    }
}