using System.Threading.Tasks;
using Deege.Events;
using UnityEngine;
using UnityEngine.UIElements;
using Deege.Game.UI;

namespace Deege.Game.UI.Custom
{
    [UIManager(UserInterface.MainMenu, UserInterfaceType.Fullscreen)]
    public class MainMenuManager : IUIManager
    {
        private MainMenuElement mainMenu;

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
            _ = ShowQuitConfirmationDialog(Application.Quit, () => { mainMenu.ActivateButtons(); PopupDocument.enabled = false; });
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
                    .SetStyleResource("LastStand.MainMenu.style")
                    .SetTitle("Doughnut Panic!")
                    .AddButton("#NEW_GAME#", "start-button", HandlePlayButtonClicked)
                    .AddButton("#SETTINGS#", "settings-button", HandleSettingsButtonClicked)
                    .AddButton("#EXIT_GAME#", "exit-button", HandleExitButtonClicked)
                    .AddButton("#ABOUT_GAME#", "about-button", HandleAboutButtonClicked)
                    .Build();
            mainMenu.Show(FullscreenDocument);
        }

        public void Hide()
        {
            mainMenu?.Hide();
        }

        async public Task ShowQuitConfirmationDialog(System.Action onConfirm, System.Action onCancel)
        {
            mainMenu.DeactivateButtons();
            if (PopupDocument == null)
            {
                Debug.LogError("Popup document not set");
                return;
            }
            PopupDocument.enabled = true;
            var confirmationDialog = ConfirmationDialogElementBuilder.Builder()
                .SetStyleResource("GameQuitConfirmationDialog.style")
                .AddButton("#CONFIRM#", "confirm", "confirm-button")
                .AddButton("#CANCEL#", "cancel", "cancel-button")
                .SetTitle("#QUIT_GAME_CONFIRMATION#")
                .SetMessage("#ARE_YOU_SURE_QUIT_GAME#")
                .Build();
            string result = await confirmationDialog.ShowAsync(PopupDocument);
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
