using CustomFailText.Services;
using Zenject;

namespace CustomFailText.Installers
{
    public class TAppInstaller : Installer<PluginConfig, TAppInstaller>
    {
        readonly PluginConfig _config;
        public TAppInstaller(PluginConfig config) => _config = config;

        public override void InstallBindings()
        {
            Container.Bind<PluginConfig>().FromInstance(_config).AsSingle();
            Container.Bind(typeof(IInitializable), typeof(ResourceHandler)).To<ResourceHandler>().AsSingle();
        }
    }
}
