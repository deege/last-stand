using Deege.Events;
using UnityEngine;
using UnityEngine.UIElements;

namespace Deege.Game.UI
{
    [UIManager(UserInterface.MainMenu, UserInterfaceType.Fullscreen)]
    public class MainMenuManager : IUIManager
    {
        private MainMenuElement mainMenu;
        private ConfirmationDialogElement confirmationDialog;

        public UIDocument SettingsDocument { get; set; }
        public UIDocument PopupDocument { get; set; }
        public UIDocument FullscreenDocument { get; set; }

        private readonly UserInterfaceChannelSO OnUserInterfaceChangeEvent;


        public UserInterface UI => UserInterface.MainMenu;
        public UserInterfaceType UIType => UserInterfaceType.Fullscreen;

        public MainMenuManager(UIDocument fullscreenDocument, UserInterfaceChannelSO onUserInterfaceChange)
        {
            this.FullscreenDocument = fullscreenDocument;
            this.OnUserInterfaceChangeEvent = onUserInterfaceChange;
        }

        private void HandlePlayButtonClicked()
        {
            Debug.Log("Play button clicked");
            // Handle play button click
        }

        private void HandleSettingsButtonClicked()
        {
            Debug.Log("Settings button clicked");
            // Handle settings button click
        }

        private void HandleExitButtonClicked()
        {
            Debug.Log("Exit button clicked");
            ShowQuitConfirmationDialog(Application.Quit, () => { mainMenu.ActivateButtons(); PopupDocument.enabled = false; });
        }

        private void HandleAboutButtonClicked()
        {
            Debug.Log("About button clicked");
            // Handle about button click
        }

        public void Show()
        {
            if (FullscreenDocument == null)
            {
                Debug.LogError("Fullscreen document not set");
                return;
            }
            mainMenu = MainMenuElementBuilder.Builder()
                    .SetTitle("Doughnut Panuc!")
                    .AddButton("#NEW_GAME#", "start-button", HandlePlayButtonClicked)
                    .AddButton("#SETTINGS#", "settings-button", HandleSettingsButtonClicked)
                    .AddButton("#EXIT_GAME#", "exit-button", HandleExitButtonClicked)
                    .AddButton("#ABOUT_GAME#", "about-button", HandleAboutButtonClicked)
                    .Build();
            mainMenu.ConstructUI(FullscreenDocument);
            mainMenu.Show();
        }

        public void Hide()
        {
            mainMenu?.Hide();
        }

        async public void ShowQuitConfirmationDialog(System.Action onConfirm, System.Action onCancel)
        {
            mainMenu.DeactivateButtons();
            if (PopupDocument == null)
            {
                Debug.LogError("Popup document not set");
                return;
            }
            PopupDocument.enabled = true;
            confirmationDialog = ConfirmationDialogElementBuilder.Builder()
                .AddButton("#CONFIRM#", "confirm", "confirm-button")
                .AddButton("#CANCEL#", "cancel", "cancel-button")
                .SetTitle("#QUIT_GAME_CONFIRMATION#")
                .SetMessage("#ARE_YOU_SURE_QUIT_GAME#")
                .Build();
            confirmationDialog.ConstructUI(PopupDocument);
            string result = await confirmationDialog.ShowAsync();
            if (result == "confirm")
            {
                onConfirm?.Invoke();
            }
            else
            {
                onCancel?.Invoke();
            }
        }
    }
}
