namespace Deege.Game.UI
{
    public enum UserInterface
    {
        MainMenu,   // The state when the player is at the main menu
        Options,    // The state for adjusting game settings
        Popup,      // A popup dialog
        Settings,   // The state for adjusting game settings
        GameOver,   // The state for when the game is over
        HUD,         // The state for the Heads Up Display
        About,      // The state for the About screen
        None,       // No UI
    }

    public enum UserInterfaceType
    {
        Fullscreen,
        Popup,
        HUD,
        Dialog,
        Notification
    }
}