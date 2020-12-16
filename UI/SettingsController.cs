using BeatSaberMarkupLanguage.Attributes;

namespace CustomFailText.UI
{
    public class SettingsController : PersistentSingleton<SettingsController>
    {
        [UIValue("pluginToggle")]
        public bool PluginToggle
        {
            get => CFTConfig.Instance.PluginToggle;
            set => CFTConfig.Instance.PluginToggle = value;
        }
    }
}
