using System.Collections.Generic;
using System.Linq;
using Refactor.TaskListKata.Entity;

namespace Refactor.TaskListKata.UseCase.Port;

public class TaskMapper
{
    public static TaskDto ToDto(Task task)
    {
        var taskDto = new TaskDto();
        taskDto.Id = task.GetId().Value;
        taskDto.Description = task.GetDescription();
        taskDto.Done = task.IsDone();
        return taskDto;
    }

    public static List<TaskDto> ToDto(List<Task> tasks)
    {
        return tasks.Select(ToDto).ToList();
    }
}