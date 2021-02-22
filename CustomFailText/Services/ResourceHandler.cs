using IPA.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Zenject;

namespace CustomFailText.Services
{
    public class ResourceHandler : IInitializable
    {
        public static List<string[]> allEntries;
        static string _filePath = $"{UnityGame.UserDataPath}\\CustomFailText\\Default.txt";
        static PluginConfig _config;

        [Inject]
        public ResourceHandler(PluginConfig config) => _config = config;

        public void Initialize()
        {
            DefaultCheck(_filePath);
            ReloadFile();
        }

        void DefaultCheck(string path)
        {
            if (!File.Exists(path))
            {
                Logger.log.Warn("No default text file found; making one now!");
                Directory.CreateDirectory(path.Replace("Default.txt", ""));
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(_defaultConf
                        .Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n"));
                    fs.Write(info, 0, info.Length);
                    Logger.log.Notice("New file created successfully!");
                }
            }
        }

        void ReloadFile()
        {
            allEntries = ReadFromFile(_config.SelectedConfig + ".txt");
            Logger.log.Info($"Config {_config.SelectedConfig} contains {allEntries.Count} entries.");
        }

        static List<string[]> ReadFromFile(string config)
        {
            List<string[]> entries = new List<string[]>();
            string path = $"{UnityGame.UserDataPath}\\CustomFailText\\";

            var linesInFile = File.ReadLines(path + config, new UTF8Encoding(true));
            linesInFile = linesInFile.Where(s => s == "" || s[0] != '#');

            List<string> currentEntry = new List<string>();
            foreach(string line in linesInFile)
            {
                if (line == "")
                {
                    entries.Add(currentEntry.ToArray());
                    currentEntry.Clear();
                }
                else currentEntry.Add(line);
            }
            if (currentEntry.Count != 0) entries.Add(currentEntry.ToArray());
            if (currentEntry.Count == 0) Logger.log.Warn("Config found, but no entries were found!");

            return entries;
        }

        const string _defaultConf =
@"# Custom Fail Text v1.2.1
# by Exomanz
#
# Use # for comments!
# Separate entries with empty lines; a random one will be picked each time you fail a song.
# Appears just like in the vanilla game:
LEVEL
FAILED

# TextMeshPro formatting (such as colors) works here!
THAT WAS
<#0080FF>B<#800000>S</color>

DON'T
PANIC

RIP

<size=+30>F

oof

<size=-5>MISSION FAILED</size>
<size=-7>we'll get 'em next time</size>

YOU
DIED

<size=+30><#800000>HECK

aw
shucks

NOPE

<size=-5>LEVEL FAILED</size>
<size=-6>Continue? <#00FF00>[$0.99]</color></size>

FISSION
MAILED

GAME
OVER

<size=-5>geeettttttt</size>
<size=-5>dunked on!!!</size>

# the ""empty"" line in this one actually has a space in it, so it doesn't separate entries
DEFEAT<size=-5>
 
PLAY OF THE GAME:</size>
<#800000>RED BLOCK</color>

<size=-5>DON'T BE UPSETTI</size>
<size=-5>HAVE SOME SPAGHETTI</size>

YOU'VE BEEN
GNOMED

ROUND
FAILED

I  II
II L

wasted
";
    }
}
