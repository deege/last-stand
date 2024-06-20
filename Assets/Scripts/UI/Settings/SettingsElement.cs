using System;
using System.Collections;
using System.Collections.Generic;
using Deege.Game.UI.Controls;
using Deege.UI.Controls;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.GlobalVariables;
using UnityEngine.UIElements;

namespace Deege.Game.UI
{
    public class SettingsElement : UIScreen
    {
        public new class UxmlFactory : UxmlFactory<SettingsElement> { }
        private const string styleResource = "Settings.style";

        private VisualElement currentCard;
        private VisualElement settingsList;
        private List<Button> buttons;
        private Dictionary<string, ISettingsCard> cards;


        public override void ConstructUI(UIDocument uiDocument)
        {
        }

        public VisualElement CreateTitleBar()
        {
            var titleBar = new VisualElement();
            titleBar.AddToClassList("title-bar");
            return titleBar;
        }

        public VisualElement CreateContentContainer()
        {
            var contentContainer = new VisualElement();
            contentContainer.AddToClassList("content-container");
            return contentContainer;
        }

        public VisualElement CreateSettingsList()
        {
            settingsList = new VisualElement();
            settingsList.AddToClassList("settings-list");
            return settingsList;
        }

        public void AddButtonToButtonList(string localizationKey, string className = "list-button")
        {
            var button = LocalizedButton.CreateButton(localizationKey, className);
            settingsList.Add(button);
            button.clicked += () => OnButtonClicked(button);
            buttons.Add(button);
        }

        private void OnButtonClicked(LocalizedButton button)
        {
            throw new NotImplementedException();
        }

        public void AddButtonToSettingsList(string key, string className)
        {
            var button = new LocalizedButton
            {
                LocalizationKey = key
            };
            button.AddToClassList(className);
            settingsList.Add(button);
        }

        public VisualElement CreateSettingsContainer()
        {
            var settingsContainer = new VisualElement();
            settingsContainer.AddToClassList("settings-container");
            return settingsContainer;
        }

        public VisualElement CreateFooterBar()
        {
            var footerBar = new VisualElement();
            footerBar.AddToClassList("footer-bar");
            return footerBar;
        }

        public void RegisterPanel(string name, ISettingsCard panel)
        {
            cards.Add(name, panel);
            (panel as VisualElement).style.display = DisplayStyle.None;
        }

        private void SwitchPanel(string name)
        {
            currentCard.style.display = DisplayStyle.None;
            currentCard = cards[name] as VisualElement;
            currentCard.style.display = DisplayStyle.Flex;
        }

        private void HideAllPanels()
        {
            foreach (var card in cards)
            {
                (card.Value as VisualElement).style.display = DisplayStyle.None;
            }
        }
    }


    public class SettingsElementBuilder
    {
        private string settingsKey = "#SETTINGS#";
        private string backKey = "#BACK#";
        private string selectKey = "#SELECT#";

        private string buttonClass = "list-button";

        private readonly List<ISettingsCard> cards = new();

        public static SettingsElementBuilder Builder()
        {
            return new SettingsElementBuilder();
        }

        public SettingsElementBuilder SetSettingsKey(string settingsKey)
        {
            this.settingsKey = settingsKey;
            return this;
        }

        public SettingsElementBuilder SetBackKey(string backKey)
        {
            this.backKey = backKey;
            return this;
        }

        public SettingsElementBuilder SetSelectKey(string selectKey)
        {
            this.selectKey = selectKey;
            return this;
        }

        public SettingsElementBuilder SetButtonClass(string buttonClass)
        {
            this.buttonClass = buttonClass;
            return this;
        }

        public SettingsElementBuilder AddPanel(ISettingsCard card)
        {
            cards.Add(card);
            return this;
        }

        public SettingsElement Build()
        {
            var settingsElement = new SettingsElement();
            return settingsElement;
        }
    }

}