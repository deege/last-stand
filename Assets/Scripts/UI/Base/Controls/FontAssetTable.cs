using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace Deege.Game.Localization
{

    [CreateAssetMenu(fileName = "FontAssetTable", menuName = "Localization/Font Asset Table")]
    public class FontAssetTable : ScriptableObject
    {
        [System.Serializable]
        public class FontEntry
        {
            public Locale locale;
            public Font font;
        }

        public List<FontEntry> fonts;

        public Font GetFontForLocale(Locale locale)
        {
            foreach (var entry in fonts)
            {
                if (entry.locale == locale)
                {
                    return entry.font;
                }
            }
            return null;
        }
    }
}