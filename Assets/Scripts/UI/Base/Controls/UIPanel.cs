using UnityEngine.UIElements;

namespace Deege.UI.Controls
{
    public class UIPanel : VisualElement, ISettingsCard
    {
        public string GetCardKey()
        {
            throw new System.NotImplementedException();
        }

        public string GetCardName()
        {
            throw new System.NotImplementedException();
        }

        public VisualElement CreateCard()
        {
            throw new System.NotImplementedException();
        }

        public void ConstructUI(UIDocument uiDocument, string styleResource = "")
        {
            throw new System.NotImplementedException();
        }

        public void Show()
        {
            style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            style.display = DisplayStyle.None;
        }

        public void Render()
        {
            throw new System.NotImplementedException();
        }

        protected void RegisterButtonEvent(string buttonName, System.Action callback)
        {
            throw new System.NotImplementedException();
        }

        protected void UnregisterButtonEvent(string buttonName)
        {
            throw new System.NotImplementedException();
        }

        protected LocalizedLabel GetLocalizedLabel(string labelName)
        {
            throw new System.NotImplementedException();
        }
    }

}