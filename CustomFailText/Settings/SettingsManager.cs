using BeatSaberMarkupLanguage.Attributes;
using IPA.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CustomFailText.Settings
{
    public class SettingsManager : PersistentSingleton<SettingsManager>
    {
        #region Basic Settings
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
        #endregion

        #region Color Manager
        [UIValue("topColor")]
        public Color TopColor
        {
            get => PluginConfig.Instance.topColor;
            set => PluginConfig.Instance.topColor = value;
        }

        [UIValue("midColor")]
        public Color MidColor
        {
            get => PluginConfig.Instance.midColor;
            set => PluginConfig.Instance.midColor = value;
        }

        [UIValue("bottomColor")]
        public Color BottomColor
        {
            get => PluginConfig.Instance.bottomColor;
            set => PluginConfig.Instance.bottomColor = value;
        }
        #endregion
    }
}