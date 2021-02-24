using CustomFailText.Services;
using System;
using Zenject;

namespace CustomFailText.Installers
{
    public class TStandardInstaller : Installer<TStandardInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<SPUpdater>().AsSingle();
        }
    }
}
