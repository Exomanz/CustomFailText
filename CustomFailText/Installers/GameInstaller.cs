using CustomFailText.Services;
using Zenject;

namespace CustomFailText.Installers
{
    public class GameInstaller : Installer<GameInstaller>
    {
        public override void InstallBindings() =>
            Container.BindInterfacesAndSelfTo<FailedEffectTextUpdater>().AsSingle();
    }
}
