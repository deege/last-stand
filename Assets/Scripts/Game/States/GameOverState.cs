

using System.Collections;
using Deege.Events;
using UnityEngine;

namespace Deege.Game
{
    public class GameOverState : IGameState
    {

        public float SecondsDelayBeforeQuit = 1.0f;
        private readonly GameControlChannelSO OnGameControlSwitch;

        public GameOverState(UserInterfaceChannelSO onUserInterfaceChangeEvent, GameControlChannelSO onGameControlSwitch)
        {
            // Code to initialize the game over state
            this.OnGameControlSwitch = onGameControlSwitch;
        }

        public void Enter()
        {
            // Code to execute when entering the game over state
        }

        public void Update()
        {
            // Code to update the game over state
        }

        public void Exit()
        {
            // Code to execute when exiting the game over state
        }

        public void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        private IEnumerator WaitBeforeQuit()
        {
            // Wait for the specified duration
            yield return new WaitForSeconds(SecondsDelayBeforeQuit);

            // Quit the application
            Debug.Log("Quitting application");
            Application.Quit();
        }
    }
}