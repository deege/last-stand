using Deege.Game;
using UnityEngine;

namespace Deege.Events
{
    [CreateAssetMenu(menuName = "Deege Events/GameState Event Channel", fileName = "GameStateEventChannelSO", order = 9)]
    public class GameStateEventChannelSO : EventChannelSO<GameState> { }
}