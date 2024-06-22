using System;
using System.Collections.Generic;
using Deege.Events;
using UnityEngine;
using UnityEngine.UIElements;

namespace Deege.Game.UI.Custom
{
    public class UIManager : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] internal UserInterfaceChannelSO OnUserInterfaceChange = default;

        [Header("Documents")]
        [SerializeField] private UIDocument fullscreenDocument;
        [SerializeField] private UIDocument popupDocument;
        [SerializeField] private UIDocument dialogDocument;
        [SerializeField] private UIDocument hudDocument;

        private readonly Dictionary<UserInterface, IUIManager> uiManagers = new();
        private readonly UICommandInvoker uiCommandInvoker = new();

        private void OnEnable()
        {
            if (fullscreenDocument == null)
            {
                Debug.LogError("Fullscreen document not set");
                return;
            }
            fullscreenDocument.enabled = true;

            if (popupDocument != null)
            {
                popupDocument.enabled = false;
            }

            if (dialogDocument != null)
            {
                dialogDocument.enabled = false;
            }

            if (hudDocument != null)
            {
                hudDocument.enabled = false;
            }

            if (OnUserInterfaceChange != null)
            {
                OnUserInterfaceChange.OnEventRaised.AddListener(OnUserInterfaceChangeEvent);
            }
        }

        private void OnDisable()
        {
            if (OnUserInterfaceChange != null)
            {
                OnUserInterfaceChange.OnEventRaised.RemoveListener(OnUserInterfaceChangeEvent);
            }
        }

        private void OnUserInterfaceChangeEvent(UserInterfaceEventArgs uiArgs)
        {
            // erwwer
            if (uiArgs.UI == UserInterface.None)
            {
                // Hide all UI
                ClearUIs();
            }
            else
            {
                switch (uiArgs.UI)
                {
                    case UserInterface.MainMenu:
                        {
                            ToggleMainMenu(uiArgs);
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void ClearUIs()
        {
            foreach (var uiManager in uiManagers.Values)
            {
                uiManager.Hide();
            }
            uiManagers.Clear();
        }

        private void ToggleMainMenu(UserInterfaceEventArgs uiArgs)
        {
            if (uiArgs.Activate)
            {
                if (!uiManagers.ContainsKey(UserInterface.MainMenu))
                {
                    MainMenuManager mainMenuManager = new(fullscreenDocument, OnUserInterfaceChange)
                    {
                        PopupDocument = popupDocument,
                        SettingsDocument = popupDocument
                    };
                    mainMenuManager.Show();
                    uiManagers.Add(UserInterface.MainMenu, mainMenuManager);
                }
            }
            else
            {
                if (uiManagers.ContainsKey(UserInterface.MainMenu))
                {
                    uiManagers[UserInterface.MainMenu].Hide();
                    uiManagers.Remove(UserInterface.MainMenu);
                }
            }
        }
    }
}
