using CustomFailText.Installers;
using IPA;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using IPAConfig = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace CustomFailText
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        [Init]
        public Plugin(IPALogger logger, IPAConfig conf, Zenjector zenjector)
        {
            Logger.log = logger;
            PluginConfig config = conf.Generated<PluginConfig>();

            zenjector.OnApp<TAppInstaller>().WithParameters(config);
            zenjector.OnMenu<TMenuInstaller>();

            //Differentiated standard and multiplayer to make the game playable
            zenjector.OnGame<TStandardInstaller>().OnlyForStandard();
            zenjector.OnGame<TMultiInstaller>().OnlyForMultiplayer();
        }

        [OnStart]
        public void OnStart() => Logger.log.Info("CustomFailText v1.2.1.2 Initialized");

        [OnExit]
        public void OnExit() { }
    }
}