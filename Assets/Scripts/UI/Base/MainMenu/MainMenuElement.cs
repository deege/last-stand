using System;
using System.Collections.Generic;
using Deege.Game.UI.Controls;
using Deege.UI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

namespace Deege.Game.UI
{
    public class MainMenuElement : UIScreen
    {
        public new class UxmlFactory : UxmlFactory<MainMenuElement> { }

        private readonly VisualElement buttonContainer = new();
        public VisualElement ButtonContainer => buttonContainer;
        public List<Button> MenuButtons { get; set; } = new();

        public string Title { get; set; }

        protected override void ConstructUI(UIDocument uiDocument, string styleResource = "MainMenu.style")
        {
            if (string.IsNullOrEmpty(styleResource))
            {
                styleResource = baseStyleResource;
            }
            styleSheets.Add(Resources.Load<StyleSheet>(styleResource));

            AddToClassList("container");
            CreateTitle();

            buttonContainer.AddToClassList("button-container");
            hierarchy.Add(buttonContainer);
            uiDocument.rootVisualElement.Add(this);
        }

        private void CreateTitle()
        {
            var gameTitle = new Label(Title);
            gameTitle.AddToClassList("title");
            hierarchy.Add(gameTitle);
        }

        public void DeactivateButtons()
        {
            foreach (Button button in MenuButtons)
            {
                button.SetEnabled(false);
            }
        }

        public void ActivateButtons()
        {
            foreach (Button button in MenuButtons)
            {
                button.SetEnabled(true);
            }
        }
    }

    public class MainMenuElementBuilder
    {
        private string _title = "Doughnut Panic!";
        private readonly List<(string, string, Action)> _buttons = new();
        private string styleResource = "MainMenu.style";

        public static MainMenuElementBuilder Builder()
        {
            return new MainMenuElementBuilder();
        }

        public MainMenuElementBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public MainMenuElementBuilder AddButton(string labelKey, string buttonClass, Action callback)
        {
            _buttons.Add((labelKey, buttonClass, callback));
            return this;
        }

        public MainMenuElementBuilder SetStyleResource(string styleResource)
        {
            this.styleResource = styleResource;
            return this;
        }

        public MainMenuElement Build()
        {
            MainMenuElement _menuScreen = new();

            // Load styles
            _menuScreen.SetStyleResource(styleResource);

            // Create title
            _menuScreen.Title = _title;

            // Add buttons
            for (var i = 0; i < _buttons.Count; i++)
            {
                var (labelKey, buttonClass, callback) = _buttons[i];
                var button = LocalizedButton.CreateButton(labelKey, buttonClass);
                _menuScreen.MenuButtons.Add(button);
                button.clicked += callback;
                _menuScreen.ButtonContainer.Add(button);
            }
            _menuScreen.ActivateButtons();

            return _menuScreen;
        }
    }
}