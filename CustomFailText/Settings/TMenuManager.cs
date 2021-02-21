using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zenject;

namespace CustomFailText.Settings
{
    public class TMenuManager : IInitializable, IDisposable
    {
        readonly MainFlowCoordinator _mainFlow;
        readonly TFlowCoordinator _tFlow;
        readonly MenuButton menuButton;

        public TMenuManager(MainFlowCoordinator mainFlow, TFlowCoordinator tFlow)
        {
            _mainFlow = mainFlow;
            _tFlow = tFlow;
            menuButton = new MenuButton("CustomFailText", SummonFlowCoordinator);
        }

        public async void Initialize()
        {
            await Task.Run(() => Thread.Sleep(100));
            MenuButtons.instance.RegisterButton(menuButton);
        }

        public void Dispose()
        {
            if (BSMLParser.IsSingletonAvailable && MenuButtons.IsSingletonAvailable)
                MenuButtons.instance.UnregisterButton(menuButton);
        }

        void SummonFlowCoordinator() => _mainFlow.PresentFlowCoordinator(_tFlow);
    }
}
