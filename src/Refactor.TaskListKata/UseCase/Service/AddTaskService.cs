using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port;
using Refactor.TaskListKata.UseCase.Port.In.Task.Add;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.UseCase.Service;

public class AddTaskService : IAddTaskUseCase
{
    private readonly IToDoListRepository _repository;
    private readonly ISystemErrorPresenter _systemErrorPresenter;

    public AddTaskService(IToDoListRepository repository, ISystemErrorPresenter systemErrorPresenter)
    {
        _repository = repository;
        _systemErrorPresenter = systemErrorPresenter;
    }

    public void Execute(AddTaskInput input)
    {
        var toDoList = _repository.FindById(new ToDoListId(input.ToDoListId));
        var projectTasks = toDoList.GetTasks(new ProjectName(input.ProjectName));

        if (projectTasks is null)
        {
            var systemErrorDto = new SystemErrorDto();
            systemErrorDto.Message = $"Could not find a project with the name \"{input.ProjectName}\".";
            _systemErrorPresenter.Present(systemErrorDto);
            return;
        }

        toDoList.AddTask(new ProjectName(input.ProjectName), input.Description, false);
        _repository.Save(toDoList);
    }
}