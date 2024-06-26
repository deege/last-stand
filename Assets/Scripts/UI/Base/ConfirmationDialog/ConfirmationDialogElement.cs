using System;
using System.Collections.Generic;
using Deege.UI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

namespace Deege.Game.UI
{
    public class ConfirmationDialogElement : UIDialog<string>
    {
        public new class UxmlFactory : UxmlFactory<ConfirmationDialogElement> { }

        public string TitleKey { get; set; } = "#MISSING_KEY#";
        public string MessageKey { get; set; } = "#MISSING_KEY#";

        protected override void ConstructUI(UIDocument uiDocument, string styleResource = "ConfirmationDialog.style")
        {
            if (string.IsNullOrEmpty(styleResource))
            {
                styleResource = baseStyleResource;
            }
            base.ConstructUI(uiDocument, styleResource);


            // Header
            VisualElement iconContainer = CreateIconContainer();
            VisualElement headerLabel = GetHeader(TitleKey);
            Header.AddToClassList("header");
            Header.Add(iconContainer);
            Header.Add(headerLabel);


            // Content
            LocalizedLabel questionLabel = CreateContent(MessageKey);
            DialogContent.Add(questionLabel);

            VisualElement content = uiDocument.rootVisualElement.Q<VisualElement>("content");
            content.Add(this);
        }

        private static LocalizedLabel CreateContent(string localizationKey = "#ARE_YOU_SURE_QUIT_GAME#")
        {
            return LocalizedLabel.CreateLabel(localizationKey, "message-label");
        }

        private static VisualElement GetHeader(string localizationKey = "#QUIT_GAME_CONFIRMATION#")
        {
            VisualElement header = new();
            header.AddToClassList("header-content");

            LocalizedLabel headerLabel = LocalizedLabel.CreateLabel(localizationKey, "header-label");
            header.Add(headerLabel);
            return header;
        }

        private VisualElement CreateIconContainer()
        {
            VisualElement iconContainer = new();
            iconContainer.AddToClassList("icon-container");
            Image icon = new();
            icon.AddToClassList("icon");
            iconContainer.Add(icon);
            Header.Add(iconContainer);
            return iconContainer;
        }
    }

    public class ConfirmationDialogElementBuilder
    {
        private readonly List<(string, string, string)> _buttons = new();
        private string _titleKey = "MISSING_KEY";
        private string _messageKey = "MISSING_KEY";
        private string styleResource = "ConfirmationDialog.style";
        private Action<UIScreen> onHide;
        private Action<UIScreen> onShow;

        public static ConfirmationDialogElementBuilder Builder()
        {
            return new ConfirmationDialogElementBuilder();
        }

        public ConfirmationDialogElementBuilder SetTitle(string title)
        {
            _titleKey = title;
            return this;
        }

        public ConfirmationDialogElementBuilder SetMessage(string message)
        {
            _messageKey = message;
            return this;
        }

        public ConfirmationDialogElementBuilder SetStyleResource(string styleResource)
        {
            this.styleResource = styleResource;
            return this;
        }

        public ConfirmationDialogElementBuilder AddButton(string labelKey, string result, string buttonClass)
        {
            _buttons.Add((labelKey, result, buttonClass));
            return this;
        }

        public ConfirmationDialogElementBuilder OnShow(Action<UIScreen> onShow)
        {
            this.onShow = onShow;
            return this;
        }

        public ConfirmationDialogElementBuilder OnHide(Action<UIScreen> onHide)
        {
            this.onHide = onHide;
            return this;
        }

        public ConfirmationDialogElement Build()
        {
            ConfirmationDialogElement _dialog = new()
            {
                TitleKey = _titleKey,
                MessageKey = _messageKey
            };
            _dialog.SetStyleResource(styleResource);

            foreach (var button in _buttons)
            {
                var (labelKey, result, buttonClass) = button;
                _dialog.AddButton(labelKey, result, buttonClass);
            }

            return _dialog;
        }
    }
}