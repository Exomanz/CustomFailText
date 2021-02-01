using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using UnityEngine;

namespace CustomFailText.Settings.UI
{
    internal class ColorSettingsViewController : BSMLResourceViewController
    {
        public override string ResourceName => "CustomFailText.Settings.Views.colorSettings.bsml";

        [UIValue("enableLights")]
        public bool EnableLights
        {
            get => PluginConfig.Instance.EnableLights;
            set => PluginConfig.Instance.EnableLights = value;
        }

        [UIValue("topLight")]
        public Color TopColor
        {
            get => PluginConfig.Instance.topColor;
            set => PluginConfig.Instance.topColor = value;
        }

        [UIValue("midLight")]
        public Color MidColor
        {
            get => PluginConfig.Instance.midColor;
            set => PluginConfig.Instance.midColor = value;
        }

        [UIValue("bottomLight")]
        public Color BottomColor
        {
            get => PluginConfig.Instance.bottomColor;
            set => PluginConfig.Instance.bottomColor = value;
        }
    }
}
