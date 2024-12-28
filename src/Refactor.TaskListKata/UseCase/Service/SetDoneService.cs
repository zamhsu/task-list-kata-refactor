using System.Linq;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port;
using Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.UseCase.Service;

public class SetDoneService : ISetDoneUseCase
{
    private readonly IToDoListRepository _repository;
    private readonly ISystemErrorPresenter _systemErrorPresenter;

    public SetDoneService(IToDoListRepository repository, ISystemErrorPresenter systemErrorPresenter)
    {
        _repository = repository;
        _systemErrorPresenter = systemErrorPresenter;
    }

    public void Execute(SetDoneInput input)
    {
        var taskId = new TaskId(input.TaskId);
        var toDoList = _repository.FindById(input.ToDoListId);
			
        if (toDoList.ContainTask(taskId) is false) {
            var systemErrorDto = new SystemErrorDto();
            systemErrorDto.Message = $"Could not find a task with an ID of {taskId}.";
            _systemErrorPresenter.Present(systemErrorDto);
            return;
        }

        toDoList.SetDone(taskId, input.Done);
    }
}