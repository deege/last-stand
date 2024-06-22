using System;

namespace Deege.Game.UI
{
    public class UserInterfaceEventArgs : EventArgs
    {
        public UserInterface UI { get; set; }
        public bool Activate { get; set; }
    }

    public class UIPopupEventArgs : UserInterfaceEventArgs
    {
        public string Message { get; set; }

    }
}