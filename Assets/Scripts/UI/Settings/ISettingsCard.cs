using Deege.UI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

namespace Deege.Game.UI
{
    public interface ISettingsCard
    {
        string GetCardKey();
        string GetCardName();
        VisualElement CreateCard();
    }
}