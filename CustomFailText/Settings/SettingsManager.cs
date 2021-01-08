using BeatSaberMarkupLanguage.Attributes;
using IPA.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CustomFailText.Settings
{
    public class SettingsManager : PersistentSingleton<SettingsManager>
    {
        [UIValue("enabled")]
        public bool Enabled
        {
            get => Configuration.config.GetBool("Custom Fail Text", "enablePlugin", true, true);
            set
            {
                Configuration.enablePlugin = value;
                Configuration.config.SetBool("Custom Fail Text", "enablePlugin", value);
            }
        }

        [UIValue("italics")]
        public bool Italics
        {
            get => Configuration.config.GetBool("Custom Fail Text", "italicText", false, true);
            set
            {
                Configuration.italicText = value;
                Configuration.config.SetBool("Custom Fail Text", "italicText", value);
            }
        }

        [UIValue("selectedConfig")]
        public string SelectedConfig
        {
            get => Configuration.config.GetString("Custom Fail Text", "config", "Default", true);
            set
            {
                Configuration.selectedConfig = value;
                Configuration.config.SetString("Custom Fail Text", "config", value);
            }
        }

        [UIValue("configList")]
        public List<object> configList = GetConfigs().ToList();
        public static List<object> GetConfigs()
        {
            List<object> configs = new List<object>();
            string configDir = $"{UnityGame.UserDataPath}\\CustomFailText\\";
            var configsAvailable = Directory.GetFiles(configDir, "*.txt");
            Plugin.Log.Debug("Available configs:");

            foreach (string config in configsAvailable)
            {
                Plugin.Log.Debug(config.Replace(configDir, "").Replace(".txt", ""));
                configs.Add(config.Replace(configDir, "").Replace(".txt", ""));
            }
            return configs;
        }

        [UIAction("#apply")]
        public void OnApply() => Plugin.Log.Notice($"Config updated to {SelectedConfig}");
    }
}