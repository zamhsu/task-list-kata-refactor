namespace Refactor.TaskListKata.UseCase.Port.Out;

public interface ISystemErrorPresenter
{
    void Present(SystemErrorDto systemErrorDto);
}