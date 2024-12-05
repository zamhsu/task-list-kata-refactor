using Refactor.TaskListKata.Entity;

namespace Refactor.TaskListKata.UseCase;

public class ShowUseCase
{
    private readonly ToDoList _toDoList;
    private readonly IConsole _console;

    public ShowUseCase(ToDoList toDoList, IConsole console)
    {
        _toDoList = toDoList;
        _console = console;
    }
    
    public void Show()
    {
        foreach (var project in _toDoList.GetProjects())
        {
            _console.WriteLine(project.GetName().ToString());
            foreach (var task in project.GetTasks())
            {
                _console.WriteLine("    [{0}] {1}: {2}", (task.IsDone() ? 'x' : ' '), task.GetId(),
                    task.GetDescription());
            }

            _console.WriteLine();
        }
    }
}