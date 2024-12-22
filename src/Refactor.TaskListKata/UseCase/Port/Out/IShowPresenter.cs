namespace Refactor.TaskListKata.UseCase.Port.Out;

public interface IShowPresenter
{
    void Present(ToDoListDto toDoListDto);
}