using System.Collections.Generic;
using System.Linq;
using Refactor.TaskListKata.Entity;

namespace Refactor.TaskListKata.UseCase.Port;

public class ProjectMapper
{
    public static ProjectDto ToDto(Project project)
    {
        var projectDto = new ProjectDto();
        projectDto.Name = project.GetName().Value;
        projectDto.TaskDtos = TaskMapper.ToDto(project.GetTasks());
        return projectDto;
    }

    public static List<ProjectDto> ToDto(List<Project> projects)
    {
        return projects.Select(ToDto).ToList();
    }
}