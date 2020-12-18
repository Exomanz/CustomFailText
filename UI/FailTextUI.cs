using BeatSaberMarkupLanguage.Attributes;

namespace CustomFailText.UI
{
    public class FailTextUI : PersistentSingleton<FailTextUI>
    {
        [UIValue("enabled")]
        public bool Enabled
        {
            get => FailTextConfig.FailConfig.GetBool("CustomFailTExt", "enablePlugin", true, true);
            set
            {
                FailTextConfig.enablePlugin = value;
                FailTextConfig.FailConfig.SetBool("Custom Fail Text", "enablePlugin", value);
            }
        }
        [UIValue("italics")]
        public bool Italics
        {
            get => FailTextConfig.FailConfig.GetBool("Custom Fail Text", "italicText", true, true);
            set
            {
                FailTextConfig.italicText = value;
                FailTextConfig.FailConfig.SetBool("Custom Fail Text", "italicText", value);
            }
        }
    }
}
