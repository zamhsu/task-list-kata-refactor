using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;

namespace Refactor.TaskListKata.UseCase.Port.Out;

public interface IHelpPresenter
{
    void Present(HelpDto helpDto);
}