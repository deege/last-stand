using UnityEngine;
using UnityEngine.Events;

namespace Deege.Events
{
    // Base class for event channels
    public abstract class EventChannelSO<T> : ScriptableObject
    {
        public UnityEvent<T> OnEventRaised = new();

        public void RaiseEvent(T value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}
