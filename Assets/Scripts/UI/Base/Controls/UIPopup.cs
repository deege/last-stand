using UnityEngine.UIElements;

namespace Deege.UI.Controls
{

    public class UIPopup : UIScreen
    {
        private LocalizedLabel titleLabel;
        private LocalizedLabel messageLabel;

        public UIPopup()
        {
            // Initialization logic will be moved to Build method
        }

        protected override void ConstructUI(UIDocument uiDocument, string styleResource = "")
        {
            base.ConstructUI(uiDocument, styleResource);
            titleLabel = new LocalizedLabel();
            titleLabel.AddToClassList("title");
            hierarchy.Add(titleLabel);

            messageLabel = new LocalizedLabel();
            messageLabel.AddToClassList("message");
            hierarchy.Add(messageLabel);
        }

        public void Initialize(string titleKey, string messageKey)
        {
            titleLabel.SetLocalizationKey(titleKey);
            messageLabel.SetLocalizationKey(messageKey);
        }
    }

}