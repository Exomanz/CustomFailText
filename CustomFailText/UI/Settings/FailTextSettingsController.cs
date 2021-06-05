using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.ViewControllers;
using CustomFailText.Services;
using IPA.Utilities;
using SiraUtil.Tools;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

namespace CustomFailText.UI.Settings
{
    [ViewDefinition("CustomFailText.UI.Settings.Views.failTextSettingsController.bsml")]
    [HotReload(RelativePathToLayout = @"..\Settings\Views\failTextSettingsController.bsml")]
    public class FailTextSettingsController : BSMLAutomaticViewController
    {
        ListBuilder _builder;
        PluginConfig _config;
        SiraLog _log;

        [Inject]
        public void Construct(ListBuilder builder, PluginConfig config, SiraLog log)
        {
            _builder = builder;
            _config = config;
            _log = log;
        }

        [UIValue("ConfigList")] public List<object> configList = ConfigLister();
        [UIComponent("DDLSConfigs")] public DropDownListSetting _ddlsConfigs;
#pragma warning disable CS0649
        [UIParams] BSMLParserParams _parserParams;
#pragma warning restore CS0649

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
            RefreshConfigList();
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
            _builder.ReadSelectedConfigFile();
        }

        internal static List<object> ConfigLister()
        {
            List<object> foundConfigs = new List<object>();
            var configsAvailable = Directory.GetFiles($@"{UnityGame.UserDataPath}\CustomFailText\", "*.txt");

            foreach (string config in configsAvailable)
                foundConfigs.Add(config.Replace(@$"{UnityGame.UserDataPath}\CustomFailText\", "").Replace(".txt", ""));

            return foundConfigs;
        }

        [UIAction("RefreshConfigList")]
        public void RefreshConfigList()
        {
            _ddlsConfigs.values = ConfigLister();
            _ddlsConfigs.UpdateChoices();
            _parserParams.EmitEvent("EmitRefreshConfigList");
        }

        #region Main Settings
        protected bool Enabled
        {
            get => _config.Enabled;
            set
            {
                _config.Enabled = value;
                NotifyPropertyChanged(nameof(Enabled));
            }
        }
        
        protected bool DisableItalics
        {
            get => _config.DisableItalics;
            set => _config.DisableItalics = value;
        }

        protected string SelectedConfig
        {
            get => _config.SelectedConfig;
            set => _config.SelectedConfig = value;
        }
        #endregion
        #region Color Manager
        protected bool LightColorer
        {
            get => _config.LightColorer;
            set
            {
                _config.LightColorer = value;
                NotifyPropertyChanged(nameof(LightColorer));
            }
        }

        protected Color TopColor
        {
            get => _config.TopColor;
            set => _config.TopColor = value;
        }

        protected Color MiddleColor
        {
            get => _config.MiddleColor;
            set => _config.MiddleColor = value;
        }

        protected Color BottomColor
        {
            get => _config.BottomColor;
            set => _config.BottomColor = value;
        }
        #endregion
    }
}
