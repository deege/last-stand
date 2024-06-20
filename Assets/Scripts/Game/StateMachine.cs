
using System.Collections.Generic;
using Deege.Events;
using UnityEngine;

namespace Deege.Game
{
    /**
     * The StateMachine class is responsible for managing the game state.
     * It contains a dictionary of game states and the current state.
     * It also has a method to change the state and notify the game state event channel.
     */
    public class StateMachine
    {
        internal GameStateEventChannelSO OnGameStateChange;
        internal UserInterfaceChannelSO OnUserInterfaceChange;

        private readonly Dictionary<GameState, IGameState> states;
        private IGameState currentState;

        public StateMachine(GameStateEventChannelSO gameStateEventChannel, UserInterfaceChannelSO onUserInterfaceChangeEvent)
        {
            this.OnGameStateChange = gameStateEventChannel;
            this.OnUserInterfaceChange = onUserInterfaceChangeEvent;
            states = new Dictionary<GameState, IGameState>()
            {
                { GameState.MainMenu, new MainMenuState(onUserInterfaceChangeEvent) },
                { GameState.Playing, new PlayingState(onUserInterfaceChangeEvent) },
                { GameState.Paused, new PausedState(onUserInterfaceChangeEvent) },
                { GameState.GameOver, new GameOverState(onUserInterfaceChangeEvent) }
            };
            currentState = null;
        }

        public void ChangeState(GameState newState)
        {
            Debug.Log($"StateMachine: ChangeState: {newState}");
            currentState?.Exit();
            currentState = states[newState];
            currentState.Enter();
            NotifyStateChange(newState);
        }

        private void NotifyStateChange(GameState newState)
        {
            if (OnGameStateChange != null)
            {
                OnGameStateChange.RaiseEvent(newState);
            }
        }

        public void Update()
        {
            currentState.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
    }
}