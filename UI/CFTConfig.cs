namespace CustomFailText.UI
{
    public class CFTConfig
    {
        public static CFTConfig Instance { get; set; }
        public bool PluginToggle { get; set; } = true;
        public bool PluginEnable { get; internal set; }
    }
}
