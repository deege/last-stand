
namespace Deege.Game.UI
{
    public interface IUICommand
    {
        void Execute();
        void Undo();
    }
}
