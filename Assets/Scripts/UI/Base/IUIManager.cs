using System;

namespace Deege.Game.UI
{
    public interface IUIManager
    {
        UserInterface UI { get; }
        UserInterfaceType UIType { get; }
        void Show();
        void Hide();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class UIManagerAttribute : Attribute
    {
        public UserInterface UI { get; }
        public UserInterfaceType UIType { get; }

        public UIManagerAttribute(UserInterface ui, UserInterfaceType uiType)
        {
            UI = ui;
            UIType = uiType;
        }
    }
}