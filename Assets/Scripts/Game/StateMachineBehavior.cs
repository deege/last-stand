
using UnityEngine;
using Deege.Events;

namespace Deege.Game
{
    /**
    Responsible for managing the state machine. It initializes the state machine 
    and calls its update and fixed update methods. It also provides a method to change 
    the state of the state machine.
     */
    public class StateMachineBehaviour : MonoBehaviour
    {
        private StateMachine stateMachine;

        [SerializeField] internal GameStateEventChannelSO OnGameStateChange;
        [SerializeField] internal UserInterfaceChannelSO OnUserInterfaceChange;
        [SerializeField] internal GameControlChannelSO OnGameControlSwitch;

        private void Awake()
        {
            Debug.Log("StateMachineBehaviour: Awake");
            // Initialize the state machine
            stateMachine = new StateMachine(OnGameStateChange, OnUserInterfaceChange, OnGameControlSwitch);
        }

        private void Start()
        {
            ChangeState(GameState.MainMenu);
        }

        private void Update()
        {
            // Call the state machine's update method
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            // Call the state machine's fixed update method
            stateMachine.FixedUpdate();
        }

        public void ChangeState(GameState newState)
        {
            // Change the state of the state machine
            stateMachine.ChangeState(newState);
        }
    }
}