using BS_Utils.Utilities;
using IPA.Utilities;
using System.Collections.Generic;
using System.IO;

namespace CustomFailText.Settings
{
    internal class Configuration
    {
        public static Config config = new Config("CustomFailText/CustomFailText");

        public static bool enablePlugin;
        public static bool italicText;
        public static string selectedConfig = config.GetString("Custom Fail Text", "config", "Default", true);
    }
}
