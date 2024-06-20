using UnityEngine;
using UnityEngine.UIElements;


namespace Deege.UI.Controls
{
    public class UINotification : UIDialog<string>
    {
        private LocalizedLabel messageLabel;

        public UINotification()
        {
            // Initialization logic will be moved to Build method
        }

        public override void ConstructUI(UIDocument uiDocument)
        {
            messageLabel = new LocalizedLabel();
            messageLabel.AddToClassList("message");
            hierarchy.Add(messageLabel);
        }

        public void Initialize(string messageKey, int duration)
        {
            messageLabel.SetLocalizationKey(messageKey);
            Show();
            RootElement.schedule.Execute(() => Hide()).StartingIn(duration * 1000);
        }
    }
}