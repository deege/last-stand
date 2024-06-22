using UnityEngine;
using UnityEngine.UIElements;

namespace Deege.Game.UI
{
    public class ShowUICommand : IUICommand
    {
        private readonly IUIManager uiManager;

        public ShowUICommand(IUIManager uiManager)
        {
            this.uiManager = uiManager;
        }

        public void Execute()
        {
            uiManager.Show();
        }

        public void Undo()
        {
            uiManager.Hide();
        }
    }
}