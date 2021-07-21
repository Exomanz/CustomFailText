using CustomFailText.Services;
using SiraUtil;
using Zenject;

namespace CustomFailText.Installers
{
    public class AppInstaller : Installer<IPA.Logging.Logger, PluginConfig, AppInstaller>
    {
        readonly PluginConfig _config;
        readonly IPA.Logging.Logger _logger;

        public AppInstaller(PluginConfig config, IPA.Logging.Logger logger)
        {
            _config = config;
            _logger = logger;
        }

        public override void InstallBindings()
        {
            Container.Bind<PluginConfig>().FromInstance(_config).AsSingle();
            Container.BindInterfacesAndSelfTo<ListBuilder>().AsSingle();
            Container.BindLoggerAsSiraLogger(_logger);
        }
    }
}
