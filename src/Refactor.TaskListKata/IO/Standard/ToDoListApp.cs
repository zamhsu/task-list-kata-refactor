using Refactor.TaskListKata.Adapter.Console;
using Refactor.TaskListKata.Adapter.Controller;
using Refactor.TaskListKata.Adapter.Presenter;
using Refactor.TaskListKata.Adapter.Repository;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.IO.Console;
using Refactor.TaskListKata.UseCase.Port.In.Project.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Show;
using Refactor.TaskListKata.UseCase.Port.Out;
using Refactor.TaskListKata.UseCase.Service;

namespace Refactor.TaskListKata.IO.Standard;

public sealed class ToDoListApp
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
    
    public static void Main(string[] args)
    {
        IConsole console = new RealConsole();
        IToDoListRepository repository = new ToDoListInMemoryRepository();
        repository.Save(new ToDoList(new ToDoListId(DEFAULT_TO_DO_LIST_ID)));
        ISystemErrorPresenter systemErrorPresenter = new SystemErrorConsolePresenter(console);
        IShowPresenter showPresenter = new ShowConsolePresenter(console);
        IShowUseCase showUseCase = new ShowService(repository, showPresenter);
        IAddProjectUseCase addProjectUseCase = new AddProjectService(repository);
        IAddTaskUseCase addTaskUseCase = new AddTaskService(repository, systemErrorPresenter);
        ISetDoneUseCase setDoneUseCase = new SetDoneService(repository, systemErrorPresenter);
        IHelpPresenter helpPresenter = new HelpConsolePresenter(console);
        IHelpUseCase helpUseCase = new HelpService(helpPresenter);
        IErrorPresenter errorPresenter = new ErrorConsolePresenter(console);
        IErrorUseCase errorUseCase = new ErrorService(errorPresenter);
        
        new ToDoListApp(console, showUseCase, addProjectUseCase, addTaskUseCase, setDoneUseCase, helpUseCase, errorUseCase).Run();
    }

    public ToDoListApp(IConsole console,
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
    }

    public void Run()
    {
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