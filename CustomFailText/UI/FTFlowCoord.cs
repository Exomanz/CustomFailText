using BeatSaberMarkupLanguage;
using CustomFailText.UI.Settings;
using HMUI;
using Zenject;

namespace CustomFailText.UI
{
    internal class FTFlowCoord : FlowCoordinator
    {
        MainFlowCoordinator _main;
        FTSettingsView _settings;
        FTCreditsView _credits;

        [Inject]
        public void Construct(MainFlowCoordinator main, FTSettingsView settings, FTCreditsView credits)
        {
            _main = main;
            _settings = settings;
            _credits = credits;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("CustomFailText", ViewController.AnimationType.In);
                showBackButton = true;
                ProvideInitialViewControllers(_settings, null, null, _credits);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            base.BackButtonWasPressed(topViewController);
            _main.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Vertical);
        }
    }
}
