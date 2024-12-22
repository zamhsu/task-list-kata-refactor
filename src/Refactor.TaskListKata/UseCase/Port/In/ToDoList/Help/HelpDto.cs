using System.Collections.Generic;

namespace Refactor.TaskListKata.UseCase.Port.In.ToDoList.Help;

public class HelpDto
{
    public string Heading { get; set; }

    public List<string> Commands { get; set; } = new List<string>();
    
    // 進度 Task List Kata 重構成整潔架構 (3) HelpUseCase
}