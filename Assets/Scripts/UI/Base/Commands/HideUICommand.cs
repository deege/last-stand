
namespace Deege.Game.UI
{
    public class HideUICommand : IUICommand
    {
        private readonly IUIManager uiManager;

        public HideUICommand(IUIManager uiManager)
        {
            this.uiManager = uiManager;
        }

        public void Execute()
        {
            uiManager.Hide();
        }

        public void Undo()
        {
            uiManager.Show();
        }
    }
}