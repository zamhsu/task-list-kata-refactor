namespace Refactor.TaskListKata.UseCase.Port.In.Task.Add;

public interface IAddTaskUseCase
{
    void Execute(AddTaskInput input);
}