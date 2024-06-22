using System.Collections.Generic;

namespace Deege.Game.UI
{
    public class UICommandInvoker
    {
        private readonly Stack<IUICommand> commandHistory = new();

        public void ExecuteCommand(IUICommand command)
        {
            command.Execute();
            commandHistory.Push(command);
        }

        public void UndoCommand()
        {
            if (commandHistory.Count > 0)
            {
                var command = commandHistory.Pop();
                command.Undo();
            }
        }
    }
}