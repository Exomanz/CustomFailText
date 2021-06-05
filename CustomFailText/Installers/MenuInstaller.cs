using CustomFailText.UI;
using CustomFailText.UI.Settings;
using SiraUtil;
using Zenject;

namespace CustomFailText.Installers
{
    public class MenuInstaller : Installer<MenuInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CFTFlowCoordinator>().FromNewComponentOnNewGameObject(nameof(CFTFlowCoordinator)).AsSingle();
            Container.Bind<FailTextSettingsController>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<FailTextCreditsController>().FromNewComponentAsViewController().AsSingle();

            Container.BindInterfacesAndSelfTo<CFTMenuButtonManager>().AsSingle();
        }
    }
}
