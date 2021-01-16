using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace CustomFailText.Settings
{
    public class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        public virtual bool Enabled { get; set; } = true;
        public virtual bool DisableItalics { get; set; } = false;
        public virtual string SelectedConfig { get; set; } = "Default";

        [UseConverter(typeof(HexColorConverter))]
        public virtual Color topColor { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        public virtual Color midColor { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        public virtual Color bottomColor { get; set; } = Color.red;

        public virtual void OnReload() { }
        public virtual void Changed() => Plugin.Log.Debug("Updated Config!");
        public virtual void CopyFrom(PluginConfig other) { }
    }
}