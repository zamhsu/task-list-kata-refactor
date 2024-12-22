using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.UseCase.Service;

public class HelpService : IHelpUseCase
{
    private readonly IHelperPresenter _presenter;

    public HelpService(IHelperPresenter presenter)
    {
        _presenter = presenter;
    }
    
    public void Execute()
    {
        var helpDto = new HelpDto();
        helpDto.Heading = "Commands:";
        helpDto.Commands.Add("show");
        helpDto.Commands.Add("add project <project name>");
        helpDto.Commands.Add("add task <project name> <task description>");
        helpDto.Commands.Add("check <task ID>");
        helpDto.Commands.Add("uncheck <task ID>");
        
        _presenter.Present(helpDto);
    }
}