
using UnityEngine.UIElements;

namespace Deege.UI.Controls
{
    public interface ISettingsCard
    {
        string GetCardKey();
        string GetCardName();
        VisualElement CreateCard();
    }
}