using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Deege.UI.Controls
{

    public class UIScreen : VisualElement
    {
        private readonly Dictionary<string, System.Action> registeredCallbacks = new();
        private readonly VisualElement rootElement;
        public VisualElement RootElement => rootElement;
        public string baseStyleResource = "Base.style";

        public event System.Action<UIScreen> OnHide;
        public event System.Action<UIScreen> OnShow;

        public UIScreen()
        {
            this.rootElement = this;
        }

        protected virtual void ConstructUI(UIDocument uiDocument, string styleResource = "")
        {
            if (!string.IsNullOrEmpty(styleResource))
            {
                styleSheets.Add(Resources.Load<StyleSheet>(styleResource));
            }
        }

        public virtual void Show(UIDocument uiDocument)
        {
            ConstructUI(uiDocument, baseStyleResource);
            style.display = DisplayStyle.Flex;
            OnShow?.Invoke(this);
        }

        public virtual void Hide()
        {
            style.display = DisplayStyle.None;
            OnHide?.Invoke(this);
        }

        public void SetStyleResource(string styleResource)
        {
            baseStyleResource = styleResource;
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

        public void SetFocusToButton(Button button)
        {
            button?.Focus();
        }

        protected LocalizedLabel GetLocalizedLabel(string labelName)
        {
            return this.Q<LocalizedLabel>(labelName);
        }
    }
}
