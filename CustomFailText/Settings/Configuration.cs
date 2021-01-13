using BS_Utils.Utilities;

namespace CustomFailText.Settings
{
    public class Configuration
    {
        internal static Config config = new Config("CustomFailText/CustomFailText");

        internal static bool enablePlugin;
        internal static bool italicText;
        internal static string selectedConfig;
        internal static string topLightColor;
        internal static string midLightColor;
        internal static string botLightColor;
        public static void RefreshSettingsOnGameStart()
        {
            enablePlugin = config.GetBool("Text", "enablePlugin");
            italicText = config.GetBool("Text", "italicText");
            selectedConfig = config.GetString("Text", "config");

            topLightColor = config.GetString("Lights", "topLightColor");
            midLightColor = config.GetString("Lights", "midLightColor");
            botLightColor = config.GetString("Lights", "botLightColor");
        }
    }
}
