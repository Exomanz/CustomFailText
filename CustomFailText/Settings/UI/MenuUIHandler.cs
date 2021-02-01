using System;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;

namespace CustomFailText.Settings.UI
{
    internal class MenuUIHandler
    {
        static FailFlowCoordinator failFlowCoordinator;
        static bool created;

        public static void CreateMenu()
        {
            if (!created)
            {
                MenuButton button = new MenuButton("Custom Fail Text", "It's your text; make it what you want", 
                    new Action(MenuButtonPressed), true);
                PersistentSingleton<MenuButtons>.instance.RegisterButton(button);
                created = true;
            }
        }
        public static void ShowSettingsFlow()
        {
            if (failFlowCoordinator == null) failFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<FailFlowCoordinator>();
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(failFlowCoordinator, null, ViewController.AnimationDirection.Horizontal, false, false);
        }

        static void MenuButtonPressed()
        {
            ShowSettingsFlow();
        }
    }
}
