using Deege.Game.Localization;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UIElements;


namespace Deege.Game.UI.Controls
{

    public class LocalizedButton : Button
    {
        private readonly LocalizedString localizedString = new();
        private FontAssetTable fontAssetTable;

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

        public LocalizedButton()
        {
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
            if (fontAssetTable != null)
            {
                OnLocaleChanged(LocalizationSettings.SelectedLocale); // Update font immediately
            }
        }

        public new class UxmlFactory : UxmlFactory<LocalizedButton, UxmlTraits> { }

        public new class UxmlTraits : Button.UxmlTraits
        {
            readonly UxmlStringAttributeDescription localizationKey = new UxmlStringAttributeDescription { name = "localization-key" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var button = ve as LocalizedButton;
                button.LocalizationKey = localizationKey.GetValueFromBag(bag, cc);
            }
        }

        static public LocalizedButton CreateButton(string localizationKey, string className)
        {
            return CreateButton(localizationKey, className, "GameStrings");
        }

        static public LocalizedButton CreateButton(string localizationKey, string className, TableReference tableReference)
        {
            var button = new LocalizedButton
            {
                name = className,
                TableReference = tableReference
            };
            button.SetLocalizationKey(localizationKey);
            button.AddToClassList("localized-button");
            button.AddToClassList(className);
            button.text = "MISSING"; // This will be overridden by localization

            return button;
        }


    }
}