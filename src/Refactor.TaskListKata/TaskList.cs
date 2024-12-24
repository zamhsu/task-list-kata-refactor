using Refactor.TaskListKata.Adapter.Controller;
using Refactor.TaskListKata.Adapter.Presenter;
using Refactor.TaskListKata.Adapter.Repository;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.UseCase;
using Refactor.TaskListKata.UseCase.Port.In.Project.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.Add;
using Refactor.TaskListKata.UseCase.Port.In.Task.SetDone;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Error;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;
using Refactor.TaskListKata.UseCase.Port.In.ToDoList.Show;
using Refactor.TaskListKata.UseCase.Port.Out;
using Refactor.TaskListKata.UseCase.Service;

namespace Refactor.TaskListKata;

public sealed class TaskList
{
    private const string QUIT = "quit";
    public const string DEFAULT_TO_DO_LIST_ID = "001";

    private readonly ToDoList _toDoList = new ToDoList(new ToDoListId(DEFAULT_TO_DO_LIST_ID));
    private readonly IConsole _console;
    private readonly IToDoListRepository _repository;
    
    private readonly IShowUseCase _showUseCase;
    private readonly IAddProjectUseCase _addProjectUseCase;
    private readonly IAddTaskUseCase _addTaskUseCase;
    private readonly ISetDoneUseCase _setDoneUseCase;
    private readonly IHelpUseCase _helpUseCase;
    private readonly IErrorUseCase _errorUseCase;
    
    public static void Main(string[] args)
    {
        new TaskList(new RealConsole()).Run();
    }

    public TaskList(IConsole console)
    {
        _console = console;
        _repository = new ToDoListInMemoryRepository();

        if (_repository.FindById(new ToDoListId(DEFAULT_TO_DO_LIST_ID)) is null)
        {
            _repository.Save(_toDoList);
            
            _showUseCase = new ShowService(_repository, new ShowConsolePresenter(_console));
            _addProjectUseCase = new AddProjectService(_repository);
            _addTaskUseCase = new AddTaskService(_repository, _console);
            _setDoneUseCase = new SetDoneService(_repository, _console);
            _helpUseCase = new HelpService(new HelpConsolePresenter(_console));
            _errorUseCase = new ErrorService(_console);
        }
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

            new ToDoListConsoleController
            (
                _showUseCase, 
                _addProjectUseCase, 
                _addTaskUseCase, 
                _setDoneUseCase,
                _helpUseCase, 
                _errorUseCase
            ).Execute(command);
        }
    }
}