using System;
using NUnit.Framework;
using Refactor.TaskListKata.Adapter.Presenter;
using Refactor.TaskListKata.Adapter.Repository;
using Refactor.TaskListKata.Entity;
using Refactor.TaskListKata.IO.Standard;
using Refactor.TaskListKata.UseCase.Service;

namespace Tasks
{
	[TestFixture]
	public sealed class ApplicationTest
	{
		public const string PROMPT = "> ";

		private FakeConsole console;
		private System.Threading.Thread applicationThread;

		[SetUp]
		public void StartTheApplication()
		{
			this.console = new FakeConsole();
			var repository = new ToDoListInMemoryRepository();
			repository.Save(new ToDoList(new ToDoListId(ToDoListApp.DEFAULT_TO_DO_LIST_ID)));
			var systemErrorPresenter = new SystemErrorConsolePresenter(console);
			var showPresenter = new ShowConsolePresenter(console);
			var showUseCase = new ShowService(repository, showPresenter);
			var addProjectUseCase = new AddProjectService(repository);
			var addTaskUseCase = new AddTaskService(repository, systemErrorPresenter);
			var setDoneUseCase = new SetDoneService(repository, systemErrorPresenter);
			var helpPresenter = new HelpConsolePresenter(console);
			var helpUseCase = new HelpService(helpPresenter);
			var errorPresenter = new ErrorConsolePresenter(console);
			var errorUseCase = new ErrorService(errorPresenter);
			
			var taskList = new ToDoListApp(console, showUseCase, addProjectUseCase, addTaskUseCase, setDoneUseCase, helpUseCase, errorUseCase);
			this.applicationThread = new System.Threading.Thread(() => taskList.Run());
			applicationThread.Start();
		}

		[TearDown]
		public void KillTheApplication()
		{
			if (applicationThread == null || !applicationThread.IsAlive)
			{
				return;
			}

			applicationThread.Abort();
			throw new Exception("The application is still running.");
		}

		[Test, Timeout(1000)]
		public void ItWorks()
		{
			Execute("show");

			Execute("add project secrets");
			Execute("add task secrets Eat more donuts.");
			Execute("add task secrets Destroy all humans.");

			Execute("show");
			ReadLines(
				"secrets",
				"    [ ] 1: Eat more donuts.",
				"    [ ] 2: Destroy all humans.",
				""
			);

			Execute("add project training");
			Execute("add task training Four Elements of Simple Design");
			Execute("add task training SOLID");
			Execute("add task training Coupling and Cohesion");
			Execute("add task training Primitive Obsession");
			Execute("add task training Outside-In TDD");
			Execute("add task training Interaction-Driven Design");

			Execute("check 1");
			Execute("check 3");
			Execute("check 5");
			Execute("check 6");

			Execute("show");
			ReadLines(
				"secrets",
				"    [x] 1: Eat more donuts.",
				"    [ ] 2: Destroy all humans.",
				"",
				"training",
				"    [x] 3: Four Elements of Simple Design",
				"    [ ] 4: SOLID",
				"    [x] 5: Coupling and Cohesion",
				"    [x] 6: Primitive Obsession",
				"    [ ] 7: Outside-In TDD",
				"    [ ] 8: Interaction-Driven Design",
				""
			);

			Execute("quit");
		}

		private void Execute(string command)
		{
			Read(PROMPT);
			Write(command);
		}

		private void Read(string expectedOutput)
		{
			var length = expectedOutput.Length;
			var actualOutput = console.RetrieveOutput(expectedOutput.Length);
			Assert.AreEqual(expectedOutput, actualOutput);
		}

		private void ReadLines(params string[] expectedOutput)
		{
			foreach (var line in expectedOutput)
			{
				Read(line + Environment.NewLine);
			}
		}

		private void Write(string input)
		{
			console.SendInput(input + Environment.NewLine);
		}
	}
}
