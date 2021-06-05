using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using IPA.Loader;
using Zenject;

namespace CustomFailText.UI.Settings
{
    [ViewDefinition("CustomFailText.UI.Settings.Views.credits.bsml")]
    [HotReload(RelativePathToLayout = @"..\Settings\Views\credits.bsml")]
    public class FailTextCreditsController : BSMLAutomaticViewController
    {
        PluginConfig _config;

        [Inject]
        public void Construct(PluginConfig config) => _config = config;

        [UIValue("PluginVer")]
        protected string PluginVer { get => _config.Version.ToString(); }
    }
}
