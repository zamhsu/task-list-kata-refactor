using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;

namespace Refactor.TaskListKata.UseCase.Port.Out;

public interface IHelperPresenter
{
    void Present(HelpDto helpDto);
}