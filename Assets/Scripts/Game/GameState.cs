namespace Deege.Game
{
    public enum GameState
    {
        MainMenu,   // The state when the player is at the main menu
        NewGame,    // Transition state for setting up a new game
        Playing,    // The state when the player is actively playing the game
        Paused,     // The game is paused
        PlayerDead, // Player has died but still has lives left, potentially a transitional state to resume playing
        GameOver,   // Player has no lives left and the game is over
        Settings     // The state for adjusting game settings
    }
}