

using Deege.Events;

namespace Deege.Game
{
    public class PausedState : IGameState
    {
        public PausedState(UserInterfaceChannelSO onUserInterfaceChangeEvent)
        {
            // Code to execute when the paused state is created
        }

        public void Enter()
        {
            // Code to execute when entering the paused state
        }

        public void Update()
        {
            // Code to update the paused state
        }

        public void Exit()
        {
            // Code to execute when exiting the paused state
        }

        public void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}