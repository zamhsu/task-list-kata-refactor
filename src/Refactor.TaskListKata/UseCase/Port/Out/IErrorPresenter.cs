using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;

namespace Refactor.TaskListKata.UseCase.Port.Out;

public interface IErrorPresenter
{
    void Present(ErrorDto errorDto);
}