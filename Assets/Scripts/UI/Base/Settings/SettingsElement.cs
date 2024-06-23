using System;
using System.Collections.Generic;
using Deege.Game.UI.Controls;
using Deege.UI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

namespace Deege.Game.UI
{
    public class SettingsElement : UIScreen
    {
        public new class UxmlFactory : UxmlFactory<SettingsElement> { }

        private UIPanel currentCard;
        private VisualElement settingsList;
        private List<Button> buttons;
        private Dictionary<string, UIPanel> cards;

        public string TitleKey { get; set; }
        public string BackKey { get; set; }
        public string SelectKey { get; set; }
        public string DefaultKey { get; set; }

        public SettingsElement()
        {
            buttons = new List<Button>();
            cards = new Dictionary<string, UIPanel>();
        }

        protected override void ConstructUI(UIDocument uiDocument, string styleResource = "Settings.style")
        {
            if (uiDocument == null)
            {
                throw new ArgumentNullException("Missing UIDocument");
            }
            if (string.IsNullOrEmpty(styleResource))
            {
                styleResource = baseStyleResource;
            }
            base.ConstructUI(uiDocument, styleResource);
            styleSheets.Add(Resources.Load<StyleSheet>(styleResource));
            AddToClassList("container");

            VisualElement titleBar = CreateTitleBar();
            VisualElement contentContainer = CreateContentContainer();
            VisualElement settingsList = CreateSettingsList();
            VisualElement settingsContainer = CreateSettingsContainer();
            VisualElement footerBar = CreateFooterBar();

            hierarchy.Add(titleBar);
            hierarchy.Add(contentContainer);
            contentContainer.Add(settingsList);
            contentContainer.Add(settingsContainer);
            hierarchy.Add(footerBar);
            uiDocument.rootVisualElement.Add(this);
        }

        public VisualElement CreateTitleBar()
        {
            var titleBar = new VisualElement();
            titleBar.AddToClassList("header");

            var title = LocalizedLabel.CreateLabel(TitleKey, "header-label");

            titleBar.Add(title);
            return titleBar;
        }

        public VisualElement CreateContentContainer()
        {
            var contentContainer = new VisualElement();
            contentContainer.AddToClassList("content-container");
            return contentContainer;
        }

        public void ActivateContentContainer()
        {
            AddToClassList("container-active");
        }

        public void DeactivateContentContainer()
        {
            RemoveFromClassList("container-active");
        }

        public VisualElement CreateSettingsList()
        {
            settingsList = new VisualElement();
            settingsList.AddToClassList("settings-buttons");
            return settingsList;
        }

        public void AddButtonToButtonList(string localizationKey, string className = "settings-menu-button")
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
            footerBar.AddToClassList("footer");

            var title = LocalizedLabel.CreateLabel(BackKey, "footer-label");
            footerBar.Add(title);

            return footerBar;
        }

        public void RegisterPanel(string name, UIPanel panel)
        {
            cards.Add(name, panel);
            panel.Hide();
        }

        public void SwitchPanel(string name)
        {
            currentCard.Hide();
            currentCard = cards[name];
            currentCard.Show();
        }

        public void HideAllPanels()
        {
            foreach (var card in cards)
            {
                card.Value.Hide();
            }
        }
    }


    public class SettingsElementBuilder
    {
        private string styleResource = "Settings.style";
        private string settingsKey = "#SETTINGS#";
        private string backKey = "#BACK#";
        private string selectKey = "#SELECT#";
        private string defaultKey = "#DEFAULT_SETTINGS#";
        private string buttonClass = "list-button";

        private Action<UIScreen> onHide;
        private Action<UIScreen> onShow;

        private readonly List<UIPanel> cards = new();

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

        public SettingsElementBuilder SetDefaultKey(string defaultKey)
        {
            this.defaultKey = defaultKey;
            return this;
        }

        public SettingsElementBuilder SetButtonClass(string buttonClass)
        {
            this.buttonClass = buttonClass;
            return this;
        }

        public SettingsElementBuilder AddSettingsCard(UIPanel card)
        {
            cards.Add(card);
            return this;
        }

        public SettingsElementBuilder SetStyleResource(string styleResource)
        {
            this.styleResource = styleResource;
            return this;
        }

        public SettingsElementBuilder OnShow(Action<UIScreen> onShow)
        {
            this.onShow = onShow;
            return this;
        }

        public SettingsElementBuilder OnHide(Action<UIScreen> onHide)
        {
            this.onHide = onHide;
            return this;
        }

        public SettingsElement Build()
        {
            var settingsElement = new SettingsElement()
            {
                TitleKey = settingsKey,
                BackKey = backKey,
                SelectKey = selectKey,
                DefaultKey = defaultKey
            };
            settingsElement.SetStyleResource(styleResource);
            settingsElement.OnShow += onShow;
            settingsElement.OnHide += onHide;

            foreach (var card in cards)
            {
                settingsElement.RegisterPanel(card.GetCardKey(), card);
            }

            return settingsElement;
        }
    }

}