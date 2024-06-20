using UnityEngine;
using UnityEngine.Events;

namespace Deege.Events
{
    // Event channel with no parameters
    [CreateAssetMenu(menuName = "Deege Events/Void Event Channel", order = 0)]
    public class VoidEventChannelSO : ScriptableObject
    {
        public UnityEvent OnEventRaised = new();

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}