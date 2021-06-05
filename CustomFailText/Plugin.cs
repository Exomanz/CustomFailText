using CustomFailText.Installers;
using IPA;
using IPA.Config.Stores;
using IPA.Loader;
using IPA.Utilities;
using IPAConfig = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;
using SiraUtil.Zenject;
using System.IO;

namespace CustomFailText
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        [Init]
        public Plugin(IPALogger logger, IPAConfig config, PluginMetadata metadata, Zenjector zenjector)
        {
            PluginConfig conf = config.Generated<PluginConfig>();
            conf.Version = metadata.Version;

            zenjector.OnApp<AppInstaller>().WithParameters(logger, conf);
            zenjector.OnMenu<Installers.MenuInstaller>();
            
            zenjector.OnGame<GameInstaller>().Expose<LevelFailedTextEffect>().OnlyForStandard();
            zenjector.OnGame<GameInstaller>(false).Expose<LevelFailedTextEffect>().OnlyForMultiplayer();

            bool dir = Directory.Exists($@"{UnityGame.UserDataPath}\CustomFailText\");
            if (!dir) Directory.CreateDirectory($@"{UnityGame.UserDataPath}\CustomFailText\");
        }

        [OnEnable]
        public void Enable()
        {
        }

        [OnDisable]
        public void Disable()
        {
        }
    }
}