
namespace Deege.Game
{
    public interface IGameState
    {
        void Enter();
        void Update();
        void FixedUpdate();
        void Exit();
    }
}
