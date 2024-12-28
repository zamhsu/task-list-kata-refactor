using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.UseCase.Service;

public class ErrorService : IErrorUseCase
{
    private readonly IErrorPresenter _presenter;

    public ErrorService(IErrorPresenter presenter)
    {
        _presenter = presenter;
    }
    
    public void Execute(ErrorInput input)
    {
        var errorDto = new ErrorDto();
        errorDto.Message = $"I don't know what the command \"{input.Command}\" is.";
        
        _presenter.Present(errorDto);
    }
}