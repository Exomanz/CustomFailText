﻿using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace CustomFailText
{
    public class PluginConfig
    {
        public virtual bool Enabled { get; set; } = true;
        public virtual bool DisableItalics { get; set; } = false;
        public virtual string SelectedConfig { get; set; } = "Default";

        public virtual bool EnableLights { get; set; } = false;
        [UseConverter(typeof(HexColorConverter))]
        public virtual Color topColor { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        public virtual Color midColor { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        public virtual Color bottomColor { get; set; } = Color.red;

        public virtual void Changed() => Logger.log.Debug("Updated Config!");
    }
}