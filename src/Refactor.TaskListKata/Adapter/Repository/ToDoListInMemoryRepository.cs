using System.Collections.Generic;
using System.Linq;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.Adapter.Repository;

public class ToDoListInMemoryRepository : IToDoListRepository
{
    private readonly List<ToDoList> _store;

    public ToDoListInMemoryRepository()
    {
        _store = new List<ToDoList>();
    }

    public void Save(ToDoList toDoList)
    {
        var existed = _store.FirstOrDefault(q => q.GetId().Equals(toDoList.GetId()));

        if (existed != null)
        {
            _store.Remove(existed);
        }
        
        _store.Add(toDoList);
    }

    public void Delete(ToDoList toDoList)
    {
        var existed = _store.FirstOrDefault(q => q.GetId().Equals(toDoList.GetId()));

        if (existed != null)
        {
            _store.Remove(existed);
        }
    }

    public ToDoList FindById(ToDoListId toDoListId)
    {
        return _store.FirstOrDefault(q => q.GetId().Equals(toDoListId));
    }
}