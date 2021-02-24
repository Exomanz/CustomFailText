using CustomFailText.Services;
using System;
using Zenject;

namespace CustomFailText.Installers
{
    public class TMultiInstaller : Installer<TMultiInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<MPUpdater>().AsSingle();
        }
    }
}
