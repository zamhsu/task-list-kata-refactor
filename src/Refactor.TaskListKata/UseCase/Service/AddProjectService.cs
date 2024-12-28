using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.In.Project.Add;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.UseCase.Service;

public class AddProjectService : IAddProjectUseCase
{
    private readonly IToDoListRepository _repository;

    public AddProjectService(IToDoListRepository repository)
    {
        _repository = repository;
    }
    
    public void Execute(AddProjectInput input)
    {
        var toDoList = _repository.FindById(new ToDoListId(input.ToDoListId));
        toDoList.AddProject(new ProjectName(input.ProjectName));
        _repository.Save(toDoList);
    }
}