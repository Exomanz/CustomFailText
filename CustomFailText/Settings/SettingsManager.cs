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
            get => Configuration.config.GetBool("Text", "enablePlugin", true, true);
            set
            {
                Configuration.enablePlugin = value;
                Configuration.config.SetBool("Text", "enablePlugin", value);
            }
        }

        [UIValue("italics")]
        public bool Italics
        {
            get => Configuration.config.GetBool("Text", "italicText", false, true);
            set
            {
                Configuration.italicText = value;
                Configuration.config.SetBool("Text", "italicText", value);
            }
        }

        [UIValue("selectedConfig")]
        public string SelectedConfig
        {
            get => Configuration.config.GetString("Text", "config", "Default", true);
            set
            {
                Configuration.selectedConfig = value;
                Configuration.config.SetString("Text", "config", value);
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

        #region Color Provider
        [UIValue("topBacklightColor")]
        public string TopBacklightColor
        {
            get => Configuration.config.GetString("Lights", "topLightColor", "Default", true);
            set
            {
                Configuration.topLightColor = value;
                Configuration.config.SetString("Lights", "topLightColor", value);
            }
        }

        [UIValue("midBacklightColor")]
        public string MidBacklightColor
        {
            get => Configuration.config.GetString("Lights", "midLightColor", "Default", true);
            set
            {
                Configuration.midLightColor = value;
                Configuration.config.SetString("Lights", "midLightColor", value);
            }
        }

        [UIValue("botBacklightColor")]
        public string BotBacklightColor
        {
            get => Configuration.config.GetString("Lights", "botLightColor", "Default", true);
            set
            {
                Configuration.botLightColor = value;
                Configuration.config.SetString("Lights", "botLightColor", value);
            }
        }
        
        [UIValue("colorsList")]
        public List<object> colorsList = new object[] { "Red", "Blue", "Cyan", "Gray", "Green", "Magenta", "Yellow", "White" }.ToList();
        #endregion
    }
}