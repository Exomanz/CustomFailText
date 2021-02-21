using CustomFailText.Services;
using System;
using Zenject;

namespace CustomFailText.Installers
{
    public class TGameInstaller : Installer<TGameInstaller>
    {
        public override void InstallBindings() =>
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<Updater>().AsSingle();
    }
}
