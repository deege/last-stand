using Deege.Events;
using Deege.Game.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Deege.Game.Player
{
    public class PlayerInputListener : MonoBehaviour
    {
        // Events
        [Header("Game Event Channels")]
        [SerializeField] internal BoolEventChannelSO OnGamePause;
        [SerializeField] internal GameControlChannelSO OnGameSwitchControls;
        [SerializeField] internal StringEventChannelSO OnLevelStart;


        [Header("Player Event Channels")]
        [SerializeField] internal Vector2EventChannelSO OnPlayerMovement;
        [SerializeField] internal VoidEventChannelSO OnPlayerSpawned;
        [SerializeField] internal VoidEventChannelSO OnPlayerDeath;

        [SerializeField] internal VoidEventChannelSO OnPlayerShootingStart;
        [SerializeField] internal VoidEventChannelSO OnPlayerShootingStop;
        [SerializeField] internal VoidEventChannelSO OnPlayerSwitchButtonPressed;

        private bool controlsAreEnabled = false;
        private InputActions inputActions;
        private InputAction moveAction;
        private InputAction fireAction;
        private InputAction switchWeaponsAction;

        private InputAction pauseAction;

        private Vector2 rawInput;

        public bool ControlsAreEnabled
        {
            get { return controlsAreEnabled; }
        }

        public void Awake()
        {

        }

        public void Update()
        {
            if (controlsAreEnabled)
            {
                OnPlayerMovement?.RaiseEvent(rawInput);
            }
        }

        public void OnEnable()
        {
            EnableInputActions();
            EnableListeners();
        }

        private void EnableInputActions()
        {
            inputActions = new InputActions();
            moveAction = inputActions.Player.Move;
            moveAction.Enable();
            inputActions.Player.Move.performed += OnPlayerMove;
            inputActions.Player.Move.canceled += OnPlayerMove;

            fireAction = inputActions.Player.Fire;
            fireAction.Enable();
            inputActions.Player.Fire.performed += OnPlayerFirePerformed;
            inputActions.Player.Fire.canceled += OnPlayerFireCanceled;

            pauseAction = inputActions.Player.Pause;
            pauseAction.Enable();
            inputActions.Player.Pause.performed += OnPlayerPause;

            switchWeaponsAction = inputActions.Player.SwitchWeapons;
            switchWeaponsAction.Enable();
            inputActions.Player.SwitchWeapons.performed += OnPlayerSwitchWeapons;
        }

        private void EnableListeners()
        {
            if (OnGamePause != null)
            {
                OnGamePause.OnEventRaised.AddListener(OnGamePauseEvent);
            }
            if (OnLevelStart != null)
            {
                OnLevelStart.OnEventRaised.AddListener(OnLevelStartEvent);
            }
            if (OnPlayerSpawned != null)
            {
                OnPlayerSpawned.OnEventRaised.AddListener(OnPlayerSpawnedEvent);
            }
            if (OnPlayerDeath != null)
            {
                OnPlayerDeath.OnEventRaised.AddListener(OnPlayerDeathEvent);
            }
        }

        private void DisableListeners()
        {
            if (OnGamePause != null)
            {
                OnGamePause.OnEventRaised.RemoveListener(OnGamePauseEvent);
            }
            if (OnLevelStart != null)
            {
                OnLevelStart.OnEventRaised.RemoveListener(OnLevelStartEvent);
            }
            if (OnPlayerSpawned != null)
            {
                OnPlayerSpawned.OnEventRaised.RemoveListener(OnPlayerSpawnedEvent);
            }
            if (OnPlayerDeath != null)
            {
                OnPlayerDeath.OnEventRaised.RemoveListener(OnPlayerDeathEvent);
            }
        }

        public void OnDisable()
        {
            DisableInputActions();
            DisableListeners();
        }

        private void DisableInputActions()
        {
            inputActions.Player.Move.performed -= OnPlayerMove;
            inputActions.Player.Move.canceled -= OnPlayerMove;

            inputActions.Player.Fire.performed -= OnPlayerFirePerformed;
            inputActions.Player.Fire.canceled -= OnPlayerFireCanceled;

            inputActions.Player.SwitchWeapons.performed -= OnPlayerSwitchWeapons;
            inputActions.Player.Pause.performed -= OnPlayerPause;
        }

        public void EnableControls()
        {
            OnGameSwitchControls?.RaiseEvent(GameControl.Player);
            controlsAreEnabled = true;
        }
        public void DisableControls()
        {
            OnGameSwitchControls?.RaiseEvent(GameControl.UI);
            OnPlayerShootingStop.RaiseEvent();
            controlsAreEnabled = false;
        }

        private void OnLevelStartEvent(string arg0)
        {
            EnableControls();
        }

        private void OnPlayerSpawnedEvent()
        {
            EnableControls();
        }

        public void OnPlayerMove(InputAction.CallbackContext obj)
        {
            if (controlsAreEnabled)
            {
                rawInput = obj.ReadValue<Vector2>();
            }
        }

        public void OnPlayerFirePerformed(InputAction.CallbackContext obj)
        {
            if (controlsAreEnabled)
            {
                OnPlayerShootingStart?.RaiseEvent();
            }
        }

        public void OnPlayerFireCanceled(InputAction.CallbackContext obj)
        {
            OnPlayerShootingStop?.RaiseEvent();
        }

        public void OnPlayerSwitchWeapons(InputAction.CallbackContext obj)
        {
            if (controlsAreEnabled)
            {
                OnPlayerSwitchButtonPressed?.RaiseEvent();
            }
        }

        public void OnPlayerPause(InputAction.CallbackContext obj)
        {
            OnGamePause?.RaiseEvent(controlsAreEnabled);
        }

        public void OnGamePauseEvent(bool pauseGame)
        {
            if (pauseGame)
            {
                DisableControls();
            }
            else
            {
                EnableControls();
            }
        }

        public void OnPlayerDeathEvent()
        {
            DisableControls();
        }
    }
}