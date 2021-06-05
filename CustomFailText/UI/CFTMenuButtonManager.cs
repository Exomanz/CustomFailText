using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using System;
using Zenject;

namespace CustomFailText.UI
{
    internal class CFTMenuButtonManager : IInitializable, IDisposable
    {
        MainFlowCoordinator _mainFlowCoordinator;
        CFTFlowCoordinator _modFlowCoordinator;
        MenuButton _menuButton;

        public CFTMenuButtonManager(MainFlowCoordinator main, CFTFlowCoordinator mod)
        {
            _mainFlowCoordinator = main;
            _modFlowCoordinator = mod;
            _menuButton = new MenuButton("CustomFailText", SummonFlowCoordinator);
        }

        public void Initialize() => MenuButtons.instance.RegisterButton(_menuButton);

        private void SummonFlowCoordinator() =>
            _mainFlowCoordinator.PresentFlowCoordinator(_modFlowCoordinator, null, HMUI.ViewController.AnimationDirection.Horizontal);

        public void Dispose()
        {
            if (BSMLParser.IsSingletonAvailable && MenuButtons.IsSingletonAvailable)
                MenuButtons.instance.UnregisterButton(_menuButton);
        }
    }
}
