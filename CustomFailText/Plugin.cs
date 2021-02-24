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
        internal static Plugin Instance { get; private set; }

        [Init]
        public Plugin(IPALogger logger, IPAConfig conf, Zenjector zenjector)
        {
            Logger.log = logger;
            PluginConfig config = conf.Generated<PluginConfig>();

            zenjector.OnApp<TAppInstaller>().WithParameters(config);
            zenjector.OnMenu<TMenuInstaller>();

            //Stupid fix for a simple problem LULW
            zenjector.OnGame<TStandardInstaller>().ShortCircuitForTutorial().ShortCircuitForMultiplayer();
            zenjector.OnGame<TMultiInstaller>().ShortCircuitForTutorial().ShortCircuitForStandard();
        }

        [OnStart]
        public void OnStart() => Logger.log.Info("CustomFailText v1.2.1.1 Initialized");

        [OnExit]
        public void OnExit() { }
    }
}