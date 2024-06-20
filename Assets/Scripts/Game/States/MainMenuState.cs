using System;
using System.Collections;
using Deege.Events;
using Deege.Game.UI;
using UnityEngine;


namespace Deege.Game
{
    public class MainMenuState : IGameState
    {

        private readonly UserInterfaceChannelSO OnUserInterfaceChangeEvent;

        public MainMenuState(UserInterfaceChannelSO onUserInterfaceChangeEvent)
        {
            this.OnUserInterfaceChangeEvent = onUserInterfaceChangeEvent;
        }

        public void Enter()
        {
            ToggleMainUI(true);
        }

        private void ToggleMainUI(bool isActive)
        {
            UserInterfaceEventArgs args = new()
            {
                UI = UserInterface.MainMenu,
                Activate = isActive
            };
            if (OnUserInterfaceChangeEvent != null)
            {
                OnUserInterfaceChangeEvent.RaiseEvent(args);
            }
        }

        public void Update()
        {

        }

        public void Exit()
        {
            ToggleMainUI(false);
        }

        public void FixedUpdate()
        {

        }


    }
}
