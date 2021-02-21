using CustomFailText.Settings;
using SiraUtil;
using Zenject;

namespace CustomFailText.Installers
{
    public class TMenuInstaller : Installer<TMenuInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<TFlowCoordinator>().FromNewComponentOnNewGameObject(nameof(TFlowCoordinator)).AsSingle();
            Container.Bind<TSettingsController>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesTo<TMenuManager>().AsSingle();
        }
    }
}
