using System.Collections;
using System.Collections.Generic;
using Deege.Events;
using Deege.Game.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Deege.Game
{
    public class GameControlsManager : MonoBehaviour
    {
        [Header("Game Event Channels")]
        [SerializeField] private GameControlChannelSO OnGameControlSwitch;
        private InputActions inputActions;


        void Awake()
        {
            inputActions = new();
        }
        public void OnEnable()
        {
            EnableListeners();
        }

        public void OnDisable()
        {
            DisableListeners();
        }

        protected void EnableListeners()
        {
            OnGameControlSwitch?.OnEventRaised.AddListener(OnGameControlSwitchEvent);
        }

        protected void DisableListeners()
        {
            OnGameControlSwitch?.OnEventRaised.RemoveListener(OnGameControlSwitchEvent);
        }

        private void OnGameControlSwitchEvent(GameControl gameControl)
        {
            EnablePlayerControls(gameControl == GameControl.Player);
            EnableUIControls(gameControl == GameControl.UI);
        }

        void EnablePlayerControls(bool isActive)
        {
            if (isActive)
            {
                inputActions.Player.Enable();
            }
            else
            {
                inputActions.Player.Disable();
            }
        }

        void EnableUIControls(bool isActive)
        {
            if (isActive)
            {
                inputActions.UI.Enable();
            }
            else
            {
                inputActions.UI.Disable();
            }
        }
    }
}