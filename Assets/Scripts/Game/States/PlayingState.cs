

using Deege.Events;

namespace Deege.Game
{
    public class PlayingState : IGameState
    {
        private readonly GameControlChannelSO OnGameControlSwitch;

        public PlayingState(UserInterfaceChannelSO onUserInterfaceChangeEvent, GameControlChannelSO onGameControlSwitch)
        {
            // Code to execute when the playing state is created
        }

        public void Enter()
        {
            // Code to execute when entering the playing state
        }

        public void Update()
        {
            // Code to update the playing state
        }

        public void Exit()
        {
            // Code to execute when exiting the playing state
        }

        public void FixedUpdate()
        {
            // Code to execute when fixed updating the playing state
        }
    }

}