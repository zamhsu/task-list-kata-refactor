using Refactor.TaskListKata.Adapter.Console;
using Refactor.TaskListKata.Adapter.Controller;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase.Port.In.Project.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Show;
using Refactor.TaskListKata.UseCase.Port.Out;

namespace Refactor.TaskListKata.IO.DependencyInjection;

public sealed class Application
{
    private const string QUIT = "quit";
    public const string DEFAULT_TO_DO_LIST_ID = "001";
    
    private readonly IConsole _console;
    private readonly IShowUseCase _showUseCase;
    private readonly IAddProjectUseCase _addProjectUseCase;
    private readonly IAddTaskUseCase _addTaskUseCase;
    private readonly ISetDoneUseCase _setDoneUseCase;
    private readonly IHelpUseCase _helpUseCase;
    private readonly IErrorUseCase _errorUseCase;
    
    public Application(IConsole console,
        IToDoListRepository repository,
        IShowUseCase showUseCase,
        IAddProjectUseCase addProjectUseCase,
        IAddTaskUseCase addTaskUseCase,
        ISetDoneUseCase setDoneUseCase,
        IHelpUseCase helpUseCase,
        IErrorUseCase errorUseCase)
    {
        _console = console;
        _showUseCase = showUseCase;
        _addProjectUseCase = addProjectUseCase;
        _addTaskUseCase = addTaskUseCase;
        _setDoneUseCase = setDoneUseCase;
        _helpUseCase = helpUseCase;
        _errorUseCase = errorUseCase;
        
        repository.Save(new ToDoList(new ToDoListId(DEFAULT_TO_DO_LIST_ID)));
    }

    public void Run()
    {
        _console.WriteLine("===== Running an console application with dependency injection. =====");
        
        while (true)
        {
            _console.Write("> ");
            var command = _console.ReadLine();
            if (command == QUIT)
            {
                break;
            }

            new ToDoListConsoleController(_showUseCase,
                _addProjectUseCase,
                _addTaskUseCase,
                _setDoneUseCase,
                _helpUseCase,
                _errorUseCase).Execute(command);
        }
    }
}