using System.Linq;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.UseCase.Service;

public class SetDoneService : ISetDoneUseCase
{
    private readonly IToDoListRepository _repository;
    private readonly IConsole _console;

    public SetDoneService(IToDoListRepository repository, IConsole console)
    {
        _repository = repository;
        _console = console;
    }

    public void Execute(SetDoneInput input)
    {
        var taskId = new TaskId(input.TaskId);
        var toDoList = _repository.FindById(input.ToDoListId);
        
        var identifiedTask = toDoList
            .GetProjects()
            .Select(project => project.GetTasks().FirstOrDefault(task => task.GetId().Equals(taskId)))
            .FirstOrDefault(task => task != null);
			
        if (identifiedTask == null) {
            _console.WriteLine("Could not find a task with an ID of {0}.", taskId);
            return;
        }

        identifiedTask.SetDone(input.Done);
    }
}