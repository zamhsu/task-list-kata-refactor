using Refactor.TaskListKata.Entity;

namespace Refactor.TaskListKata.UseCase.Port.Out;

public interface IToDoListRepository
{
    void Save(ToDoList toDoList);

    void Delete(ToDoList toDoList);

    ToDoList FindById(ToDoListId toDoListId);
}