using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Show;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.UseCase.Service;

public class ShowService : IShowUseCase
{
    private readonly IToDoListRepository _repository;
    private readonly IShowPresenter _presenter;

    public ShowService(IToDoListRepository repository, IShowPresenter presenter)
    {
        _repository = repository;
        _presenter = presenter;
    }
    
    public void Execute(ShowInput input)
    {
        var toDoList = _repository.FindById(new ToDoListId(input.ToDoListId));
        var toDoListDto = ToDoListMapper.ToDto(toDoList);
        
        _presenter.Present(toDoListDto);
    }
}