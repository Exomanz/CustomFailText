using BeatSaberMarkupLanguage;
using HMUI;
using Zenject;

namespace CustomFailText.Settings
{
    public class TFlowCoordinator : FlowCoordinator
    {
        TSettingsController _controller;
        MainFlowCoordinator _mainFlow;

        [Inject]
        public void Construct(TSettingsController controller, MainFlowCoordinator mainFlow)
        {
            _controller = controller;
            _mainFlow = mainFlow;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("CustomFailText Options", ViewController.AnimationType.In);
                showBackButton = true;
                ProvideInitialViewControllers(_controller);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            base.BackButtonWasPressed(topViewController);
            _mainFlow.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal, false);
        }
    }
}
