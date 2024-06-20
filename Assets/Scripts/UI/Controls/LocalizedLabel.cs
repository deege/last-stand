using System;
using Deege.Game.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UIElements;

namespace Deege.UI.Controls
{
    public class LocalizedLabel : Label
    {
        private readonly LocalizedString localizedString = new();
        private FontAssetTable fontAssetTable;

        private readonly float baseFontSize;

        public TableReference TableReference
        {
            get => localizedString.TableReference;
            set => localizedString.TableReference = value;
        }

        public string LocalizationKey
        {
            get => localizedString.TableEntryReference.Key;
            set
            {
                localizedString.TableEntryReference = value;
                localizedString.StringChanged += UpdateText;
            }
        }

        public LocalizedLabel()
        {
            baseFontSize = style.fontSize.value.value;
            RegisterCallback<AttachToPanelEvent>(evt => Initialize());
            RegisterCallback<DetachFromPanelEvent>(evt => Cleanup());
        }

        private void Initialize()
        {
            localizedString.StringChanged += UpdateText;
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
            UpdateText(localizedString.GetLocalizedString());
            OnLocaleChanged(LocalizationSettings.SelectedLocale);
        }

        private void Cleanup()
        {
            localizedString.StringChanged -= UpdateText;
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        private void UpdateText(string localizedString)
        {
            text = localizedString;
        }

        private void OnLocaleChanged(Locale locale)
        {
            if (fontAssetTable != null)
            {
                var font = fontAssetTable.GetFontForLocale(locale);
                if (font != null)
                {
                    style.unityFont = font;
                }
            }
        }

        public void SetLocalizationKey(string key)
        {
            LocalizationKey = key;
        }

        public void SetFontAssetTable(FontAssetTable table)
        {
            fontAssetTable = table;
            OnLocaleChanged(LocalizationSettings.SelectedLocale); // Update font immediately
        }

        public void ScaleFontSize(float scaleFactor)
        {
            float scaleFactorUsed = Mathf.Clamp(scaleFactor, 0.5f, 2.0f);
            style.fontSize = baseFontSize * scaleFactorUsed;
        }

        static public LocalizedLabel CreateLabel(string localizationKey, string className)
        {
            return CreateLabel(localizationKey, className, "GameStrings");
        }

        static public LocalizedLabel CreateLabel(string localizationKey, string className, TableReference tableReference)
        {
            var label = new LocalizedLabel
            {
                name = className,
                TableReference = tableReference
            };
            label.SetLocalizationKey(localizationKey);
            label.AddToClassList("localized-label");
            label.AddToClassList(className);
            label.text = "MISSING"; // This will be overridden by localization

            return label;
        }

        public new class UxmlFactory : UxmlFactory<LocalizedLabel, UxmlTraits> { }

        public new class UxmlTraits : Label.UxmlTraits
        {
            readonly UxmlStringAttributeDescription localizationKey = new() { name = "localization-key" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var label = ve as LocalizedLabel;
                label.LocalizationKey = localizationKey.GetValueFromBag(bag, cc);
            }

        }
    }
}