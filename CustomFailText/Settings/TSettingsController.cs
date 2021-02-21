using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using IPA.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CustomFailText.Settings
{
    [ViewDefinition("CustomFailText.Settings.Views.mainSettings.bsml")]
    public class TSettingsController : BSMLAutomaticViewController
    {
        PluginConfig _config;
        static string confDir = $"{UnityGame.UserDataPath}\\CustomFailText\\";

        [Inject]
        public void Construct(PluginConfig config) => _config = config;

        [UIValue("enabled")]
        protected bool Enabled
        {
            get => _config.Enabled;
            set
            {
                _config.Enabled = value;
                NotifyPropertyChanged(nameof(FTIsEnabled));
            }
        }

        [UIValue("italics")]
        protected bool Italics
        {
            get => _config.DisableItalics;
            set => _config.DisableItalics = value;
        }

        [UIValue("config")]
        protected string Config
        {
            get => _config.SelectedConfig;
            set => _config.SelectedConfig = value;
        }

        [UIValue("configList")]
        public List<object> ConfigList = GetConfigs(confDir).ToList();
        public static List<object> GetConfigs(string dir)
        {
            List<object> configs = new List<object>();
            var configsAvailable = Directory.GetFiles(dir, "*.txt");
            Logger.log.Debug("Available Configs:");

            foreach (string config in configsAvailable)
            {
                string c = config.Replace(dir, "").Replace(".txt", "");
                Logger.log.Debug(c);
                configs.Add(c);
            }
            return configs;
        }

        [UIValue("lights")]
        protected bool Lights
        {
            get => _config.EnableLights;
            set
            {
                _config.EnableLights = value;
                NotifyPropertyChanged(nameof(FTLightsEnabled));
            }
        }

        [UIValue("topCol")]
        protected Color TopCol
        {
            get => _config.topColor;
            set => _config.topColor = value;
        }

        [UIValue("midCol")]
        protected Color MidCol
        {
            get => _config.midColor;
            set => _config.midColor = value;
        }

        [UIValue("bottomCol")]
        protected Color BottomCol
        {
            get => _config.bottomColor;
            set => _config.bottomColor = value;
        }

        [UIValue("ftIsEnabled")]
        protected bool FTIsEnabled
        {
            get
            {
                return Enabled switch
                {
                    true => true,
                    false => false
                };
            }
        }

        [UIValue("ftLightsEnabled")]
        protected bool FTLightsEnabled
        {
            get
            {
                return Lights switch
                {
                    true => true,
                    false => false
                };
            }
        }
    }
}
