using System.Linq;
using Refactor.TaskListKata.Entity;

namespace Refactor.TaskListKata.UseCase;

public class SetDoneUseCase
{
    private readonly ToDoList _toDoList;
    private readonly IConsole _console;

    public SetDoneUseCase(ToDoList toDoList, IConsole console)
    {
        _toDoList = toDoList;
        _console = console;
    }
        
    public void SetDone(string idString, bool done)
    {
        var id = new TaskId(idString);
        var identifiedTask = _toDoList
            .GetProjects()
            .Select(project => project.GetTasks().FirstOrDefault(task => task.GetId().Equals(id)))
            .FirstOrDefault(task => task != null);
			
        if (identifiedTask == null) {
            _console.WriteLine("Could not find a task with an ID of {0}.", id);
            return;
        }

        identifiedTask.SetDone(done);
    }
}