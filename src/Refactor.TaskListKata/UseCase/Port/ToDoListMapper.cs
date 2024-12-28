using Refactor.TaskListKata.Entity;

namespace Refactor.TaskListKata.UseCase.Port;

public class ToDoListMapper
{
    public static ToDoListDto ToDto(ToDoList toDoList)
    {
        var toDoListDto = new ToDoListDto();
        toDoListDto.Id = toDoList.GetId().Value;
        toDoListDto.ProjectDtos = ProjectMapper.ToDto(toDoList.GetProjects());
        return toDoListDto;
    }
}