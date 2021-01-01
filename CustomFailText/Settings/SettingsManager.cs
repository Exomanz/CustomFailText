 using BeatSaberMarkupLanguage.Attributes;

namespace CustomFailText.Settings
{
    public class SettingsManager : PersistentSingleton<SettingsManager>
    {
        [UIValue("enabled")]
        public virtual bool Enabled
        {
            get => Configuration.config.GetBool("Custom Fail Text", "enablePlugin", true, true);
            set
            {
                Configuration.enablePlugin = value;
                Configuration.config.SetBool("Custom Fail Text", "enablePlugin", value);
            }
        }

        [UIValue("italics")]
        public virtual bool Italics
        {
            get => Configuration.config.GetBool("Custom Fail Text", "italicText", false, true);
            set
            {
                Configuration.italicText = value;
                Configuration.config.SetBool("Custom Fail Text", "italicText", value);
            }
        }

        [UIAction("reloadDefault")]
        private void ReloadDefault()
        {
            Plugin.Log.Info("Reloading default config file!");
            Plugin.ReadFromFile();
        }

        [UIAction("dummyButton")]
        private void DummyButton()
        {
            Plugin.Log.Debug("I told you it didn't do anything! Don't say I didn't warn you.");
        }
    }
}
