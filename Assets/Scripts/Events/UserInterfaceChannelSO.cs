using Deege.Game;
using Deege.Game.UI;
using UnityEngine;

namespace Deege.Events
{
    [CreateAssetMenu(menuName = "Deege Events/UserInterface Event Channel", fileName = "UserInterfaceChannelSO", order = 12)]
    public class UserInterfaceChannelSO : EventChannelSO<UserInterfaceEventArgs> { }
}