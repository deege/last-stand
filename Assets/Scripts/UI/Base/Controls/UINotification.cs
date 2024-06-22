using UnityEngine;
using UnityEngine.UIElements;


namespace Deege.UI.Controls
{
    public class UINotification : UIDialog<string>
    {
        private LocalizedLabel messageLabel;
        private UIDocument uiDocument;

        public UINotification()
        {
            // Initialization logic will be moved to Build method
        }

        protected override void ConstructUI(UIDocument parentDocument, string styleResource = "")
        {
            uiDocument = parentDocument;
            messageLabel = new LocalizedLabel();
            messageLabel.AddToClassList("message");
            hierarchy.Add(messageLabel);
        }

        public void Initialize(string messageKey, int duration, UIDocument parentDocument)
        {
            messageLabel.SetLocalizationKey(messageKey);
            Show(parentDocument);
            RootElement.schedule.Execute(() => Hide()).StartingIn(duration * 1000);
        }
    }
}