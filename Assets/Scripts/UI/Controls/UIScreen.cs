using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Deege.UI.Controls
{

    public class UIScreen : VisualElement
    {
        private readonly Dictionary<string, System.Action> registeredCallbacks = new();
        private readonly VisualElement rootElement;
        public VisualElement RootElement => rootElement;

        public UIScreen()
        {
            this.rootElement = this;
        }

        public virtual void ConstructUI(UIDocument uiDocument)
        {
            // To be overridden by derived classes for specific setup
        }

        public virtual void Show()
        {
            style.display = DisplayStyle.Flex;
        }

        public virtual void Hide()
        {
            style.display = DisplayStyle.None;
        }

        public virtual void Render()
        {
            // Custom rendering logic if needed
        }

        protected void RegisterButtonEvent(string buttonName, System.Action callback)
        {
            var button = this.Q<Button>(buttonName);
            if (button != null && !registeredCallbacks.ContainsKey(buttonName))
            {
                button.clicked += callback;
                registeredCallbacks.Add(buttonName, callback);
            }
        }

        protected void UnregisterButtonEvent(string buttonName)
        {
            var button = this.Q<Button>(buttonName);
            if (button != null && registeredCallbacks.ContainsKey(buttonName))
            {
                button.clicked -= registeredCallbacks[buttonName];
                registeredCallbacks.Remove(buttonName);
            }
        }

        protected LocalizedLabel GetLocalizedLabel(string labelName)
        {
            return this.Q<LocalizedLabel>(labelName);
        }
    }
}
