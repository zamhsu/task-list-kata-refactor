using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.In.Task.Add;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.UseCase.Service;

public class AddTaskService : IAddTaskUseCase
{
    private readonly IToDoListRepository _repository;
    private readonly IConsole _console;

    public AddTaskService(IToDoListRepository repository, IConsole console)
    {
        _repository = repository;
        _console = console;
    }

    public void Execute(AddTaskInput input)
    {
        var toDoList = _repository.FindById(new ToDoListId(input.ToDoListId));
        var projectTasks = toDoList.GetTasks(new ProjectName(input.ProjectName));

        if (projectTasks is null)
        {
            _console.WriteLine("Could not find a project with the name \"{0}\".", input.ProjectName);
            return;
        }

        toDoList.AddTask(new ProjectName(input.ProjectName), input.Description, false);
        _repository.Save(toDoList);
    }
}