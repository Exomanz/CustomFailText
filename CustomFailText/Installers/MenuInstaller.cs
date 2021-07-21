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
            Container.Bind<FTFlowCoord>().FromNewComponentOnNewGameObject(nameof(FTFlowCoord)).AsSingle();
            Container.Bind<FTSettingsView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<FTCreditsView>().FromNewComponentAsViewController().AsSingle();

            Container.BindInterfacesAndSelfTo<FTMenuButtons>().AsSingle();
        }
    }
}
