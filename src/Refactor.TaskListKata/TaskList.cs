using System;
using System.Linq;
using Refactor.TaskListKata.Entity;

namespace Refactor.TaskListKata
{
    public sealed class TaskList
    {
        private const string QUIT = "quit";
        private const string DEFAULT_TO_DO_LIST_ID = "001";

        private readonly ToDoList toDoList = new ToDoList(new ToDoListId(DEFAULT_TO_DO_LIST_ID));
        private readonly IConsole console;

        private long lastId = 0;
        public static void Main(string[] args)
        {
            new TaskList(new RealConsole()).Run();
        }

        public TaskList(IConsole console)
        {
            this.console = console;
        }

        public void Run()
        {
            while (true)
            {
                console.Write("> ");
                var command = console.ReadLine();
                if (command == QUIT)
                {
                    break;
                }

                Execute(command);
            }
        }

        private void Execute(string commandLine)
        {
            var commandRest = commandLine.Split(" ".ToCharArray(), 2);
            var command = commandRest[0];
            switch (command)
            {
                case "show":
                    Show();
                    break;
                case "add":
                    Add(commandRest[1]);
                    break;
                case "check":
                    Check(commandRest[1]);
                    break;
                case "uncheck":
                    Uncheck(commandRest[1]);
                    break;
                case "help":
                    Help();
                    break;
                default:
                    Error(command);
                    break;
            }
        }

        private void Show()
        {
            foreach (var project in toDoList.GetProjects())
            {
                console.WriteLine(project.GetName().ToString());
                foreach (var task in project.GetTasks())
                {
                    console.WriteLine("    [{0}] {1}: {2}", (task.IsDone() ? 'x' : ' '), task.GetId(),
                        task.GetDescription());
                }

                console.WriteLine();
            }
        }

        private void Add(string commandLine)
        {
            var subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
            var subcommand = subcommandRest[0];
            if (subcommand == "project")
            {
                AddProject(new ProjectName(subcommandRest[1]));
            }
            else if (subcommand == "task")
            {
                var projectTask = subcommandRest[1].Split(" ".ToCharArray(), 2);
                AddTask(new ProjectName(projectTask[0]), projectTask[1]);
            }
        }

        private void AddProject(ProjectName projectName)
        {
            toDoList.AddProject(projectName);
        }

        private void AddTask(ProjectName projectName, string description)
        {
            var projectTasks = toDoList.GetTasks(projectName);

            if (projectTasks is null)
            {
                Console.WriteLine("Could not find a project with the name \"{0}\".", projectName);
                return;
            }

            toDoList.AddTask(projectName, description, false);
        }

        private void Check(string idString)
        {
            SetDone(idString, true);
        }

        private void Uncheck(string idString)
        {
            SetDone(idString, false);
        }

        private void SetDone(string idString, bool done)
        {
            var id = new TaskId(idString);
            var identifiedTask = toDoList
                .GetProjects()
                .Select(project => project.GetTasks().FirstOrDefault(task => task.GetId().Equals(id)))
                .FirstOrDefault(task => task != null);

            if (identifiedTask == null)
            {
                console.WriteLine("Could not find a task with an ID of {0}.", id);
                return;
            }

            identifiedTask.SetDone(done);
        }

        private void Help()
        {
            console.WriteLine("Commands:");
            console.WriteLine("  show");
            console.WriteLine("  add project <project name>");
            console.WriteLine("  add task <project name> <task description>");
            console.WriteLine("  check <task ID>");
            console.WriteLine("  uncheck <task ID>");
            console.WriteLine();
        }

        private void Error(string command)
        {
            console.WriteLine("I don't know what the command \"{0}\" is.", command);
        }

        private long NextId()
        {
            return ++lastId;
        }
    }
}