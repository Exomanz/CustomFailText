using System;
using BeatSaberMarkupLanguage;
using HMUI;

namespace CustomFailText.Settings.UI
{
    internal class FailFlowCoordinator : FlowCoordinator
    {
        MainSettingsViewController mainSettingsView;
        ColorSettingsViewController colorSettingsView;

        public void Awake()
        {
            if (!mainSettingsView) mainSettingsView = BeatSaberUI.CreateViewController<MainSettingsViewController>();
            if (!colorSettingsView) colorSettingsView = BeatSaberUI.CreateViewController<ColorSettingsViewController>();
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try
            {
                if (firstActivation)
                {
                    SetTitle("Custom Fail Text", ViewController.AnimationType.In);
                    showBackButton = true;
                    ProvideInitialViewControllers(mainSettingsView, colorSettingsView);
                }
            }
            catch (Exception ex) { Plugin.Log.Error(ex); }
        }

        protected override void BackButtonWasPressed(ViewController topViewController) => 
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator
                (this, null, ViewController.AnimationDirection.Horizontal, false);
    }
}
