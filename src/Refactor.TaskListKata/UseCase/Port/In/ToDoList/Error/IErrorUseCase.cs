namespace Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;

public interface IErrorUseCase
{
    void Execute(ErrorInput input);
}