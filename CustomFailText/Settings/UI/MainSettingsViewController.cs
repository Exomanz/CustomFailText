using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;
using System.Linq;
using System.Collections.Generic;
using IPA.Utilities;
using System.IO;

namespace CustomFailText.Settings.UI
{
    internal class MainSettingsViewController : BSMLResourceViewController
    {
        public override string ResourceName => "CustomFailText.Settings.Views.mainSettings.bsml";

        [UIValue("enabled")]
        public bool Enabled
        {
            get => PluginConfig.Instance.Enabled;
            set => PluginConfig.Instance.Enabled = value;
        }

        [UIValue("disableItalics")]
        public bool DisableItalics
        {
            get => PluginConfig.Instance.DisableItalics;
            set => PluginConfig.Instance.DisableItalics = value;
        }

        [UIValue("selectedConfig")]
        public string SelectedConfig
        {
            get => PluginConfig.Instance.SelectedConfig;
            set => PluginConfig.Instance.SelectedConfig = value;
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

        [UIAction("coolVoid")]
        void CoolThing() => Plugin.Log.Debug("I don't to anything yet, but just you wait! One day will come my day of reckoning.");
    }
}
