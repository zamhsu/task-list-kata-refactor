namespace Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;

public interface ISetDoneUseCase
{
    void Execute(SetDoneInput input);
}