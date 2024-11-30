using System;

namespace Refactor.TaskListKata
{
	public interface IConsole
	{
		string ReadLine();

		void Write(string format, params object[] args);

		void WriteLine(string format, params object[] args);

		void WriteLine();
	}
}
