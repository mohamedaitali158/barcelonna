using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Ashfall.Localization
{
    public enum SupportedLanguage { Arabic, English, French, Spanish, Japanese, Chinese, Russian }

    /// <summary>
    /// Runtime localization manager with fallback and RTL detection.
    /// Requires package: com.unity.localization
    /// </summary>
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField] private SupportedLanguage fallbackLanguage = SupportedLanguage.English;
        public event Action<SupportedLanguage, bool> OnLanguageChanged;

        private static readonly Dictionary<SupportedLanguage, string> LocaleCodes = new()
        {
            { SupportedLanguage.Arabic, "ar" },
            { SupportedLanguage.English, "en" },
            { SupportedLanguage.French, "fr" },
            { SupportedLanguage.Spanish, "es" },
            { SupportedLanguage.Japanese, "ja" },
            { SupportedLanguage.Chinese, "zh" },
            { SupportedLanguage.Russian, "ru" }
        };

        public bool IsRtlLanguage(SupportedLanguage lang) => lang == SupportedLanguage.Arabic;

        public async Task InitializeAsync()
        {
            await LocalizationSettings.InitializationOperation.Task;
            await SetLanguageAsync(fallbackLanguage);
        }

        public async Task SetLanguageAsync(SupportedLanguage lang)
        {
            await LocalizationSettings.InitializationOperation.Task;
            var locale = LocalizationSettings.AvailableLocales.Locales
                .FirstOrDefault(l => l.Identifier.Code.StartsWith(LocaleCodes[lang], StringComparison.OrdinalIgnoreCase));

            if (locale == null)
            {
                locale = LocalizationSettings.AvailableLocales.Locales
                    .FirstOrDefault(l => l.Identifier.Code.StartsWith(LocaleCodes[fallbackLanguage], StringComparison.OrdinalIgnoreCase));
                lang = fallbackLanguage;
            }

            LocalizationSettings.SelectedLocale = locale;
            OnLanguageChanged?.Invoke(lang, IsRtlLanguage(lang));
        }
    }
}
