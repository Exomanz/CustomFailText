using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using SemVer;
using SiraUtil.Converters;
using UnityEngine;

namespace CustomFailText
{
    public class PluginConfig
    {
        [NonNullable, UseConverter(typeof(VersionConverter))]
        public virtual Version Version { get; set; }

        //Base Settings
        public virtual bool Enabled { get; set; } = true;
        public virtual bool DisableItalics { get; set; } = true;
        public virtual string SelectedConfig { get; set; } = "Default";

        //Color Manager
        public virtual bool LightColorer { get; set; } = false;
        [UseConverter(typeof(HexColorConverter))] public virtual Color TopColor { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))] public virtual Color MiddleColor { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))] public virtual Color BottomColor { get; set; } = Color.red;
    }
}
