using System;
using System.Threading.Tasks;
using Deege.Events;
using Deege.Game.Inputs;
using Deege.UI.Controls;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


namespace Deege.Game.UI.Custom
{
    [UIManager(UserInterface.MainMenu, UserInterfaceType.Fullscreen)]
    public class MainMenuManager : IUIManager
    {
        private MainMenuElement mainMenu;
        private readonly InputActions inputActions;

        public UIDocument SettingsDocument { get; set; }
        public UIDocument PopupDocument { get; set; }
        public UIDocument FullscreenDocument { get; set; }
        private UIScreen currentScreen;

        private readonly UserInterfaceChannelSO OnUserInterfaceChangeEvent;


        public UserInterface UI => UserInterface.MainMenu;
        public UserInterfaceType UIType => UserInterfaceType.Fullscreen;
        private Action<InputAction.CallbackContext> cancelCallback;

        public MainMenuManager(UIDocument fullscreenDocument, UserInterfaceChannelSO onUserInterfaceChange)
        {
            this.FullscreenDocument = fullscreenDocument;
            this.OnUserInterfaceChangeEvent = onUserInterfaceChange;
            this.cancelCallback = (context) =>
            {
                Debug.Log("Cancel button clicked");
                currentScreen?.Hide();
            };
            inputActions = new InputActions();
            inputActions.Enable();
            inputActions.UI.Cancel.performed += cancelCallback;
        }

        private void HandlePlayButtonClicked()
        {
            Debug.Log("Play button clicked");
            // Handle play button click
        }

        private void HandleSettingsButtonClicked()
        {
            Debug.Log("Settings button clicked");
            if (SettingsDocument == null)
            {
                Debug.LogError("Settings document not set");
                return;
            }
            SettingsDocument.enabled = true;
            mainMenu.DeactivateButtons();
            var settingsElement = SettingsElementBuilder.Builder()
                .SetStyleResource("GameSettings.style")
                .SetSettingsKey("#SETTINGS#")
                .OnShow((screen) =>
                {
                    (screen as SettingsElement).ActivateContentContainer();
                    currentScreen = screen;
                })
                .OnHide((screen) =>
                {
                    (screen as SettingsElement).DeactivateContentContainer();
                    mainMenu.ActivateButtons();
                    SettingsDocument.enabled = false;
                    currentScreen = null;
                })
                .Build();

            settingsElement.Show(SettingsDocument);
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
                .OnShow((screen) =>
                {
                    currentScreen = screen;
                })
                .OnHide((screen) =>
                {
                    mainMenu.ActivateButtons();
                    SettingsDocument.enabled = false;
                    currentScreen = null;
                })
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
            PopupDocument.enabled = false;
        }
    }
}
