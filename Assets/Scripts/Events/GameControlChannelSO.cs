using Deege.Game;
using UnityEngine;

namespace Deege.Events
{
    [CreateAssetMenu(menuName = "Deege Events/GameControl Event Channel", fileName = "GameControlEventChannelSO", order = 10)]
    public class GameControlChannelSO : EventChannelSO<GameControl> { }
}