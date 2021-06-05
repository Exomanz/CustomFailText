using IPA.Utilities;
using SiraUtil.Tools;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using UnityEngine.SceneManagement;
using Zenject;

namespace CustomFailText.Services
{
    public class ListBuilder : IInitializable
    {
        public List<string[]> allEntries = null;
        string defDirPath = null;
        string defTextPath = null;
        PluginConfig _config;
        SiraLog _log;

        public ListBuilder(SiraLog log, PluginConfig config)
        {
            _log = log;
            _config = config;

            _log.Logger.Debug("Initializing List Builder");
            defDirPath = $@"{UnityGame.UserDataPath}\CustomFailText\";
            defTextPath = $@"{defDirPath}Default.txt";
        }

        public void Initialize()
        {
            SceneManager.activeSceneChanged += ActiveSceneChanged;
            DefaultFileChecker();
        }

        public void DefaultFileChecker()
        {
            if (Directory.Exists(defDirPath) && File.Exists(defTextPath))
            {
                _log.Logger.Debug("Default directory and text file exist!");
                ReadSelectedConfigFile();
                return;
            }

            else
            {
                if (!Directory.Exists(defDirPath))
                {
                    _log.Logger.Warn("Default directory does not exist. Making it now.");
                    Directory.CreateDirectory(defDirPath);
                    _log.Logger.Notice("New directory made!");
                }

                if (!File.Exists(defTextPath))
                {
                    _log.Logger.Warn("Default text file does not exist. Making one now.");
                    using (FileStream fs = File.Create(defTextPath))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(_defaultConfig);
                        fs.Write(info, 0, info.Length);
                    }
                    _log.Logger.Notice("New text file made!");
                }

                ReadSelectedConfigFile();
            }
        }

        internal void ActiveSceneChanged(Scene sceneName, Scene _)
            { if (sceneName.name == "MenuEnvironment") ReadSelectedConfigFile(); }

        internal void ReadSelectedConfigFile()
        {
            try
            {
                allEntries = null;
                allEntries = ReadFromFile(_config.SelectedConfig);
            }
            catch
            { 
                _log.Logger.Error($"Config '{_config.SelectedConfig}' does not or no longer exists, and was likely deleted.");
                _log.Logger.Error("Switching to 'Default.");
                _config.SelectedConfig = "Default";
                DefaultFileChecker();
            }
        }

        internal List<string[]> ReadFromFile(string configName)
        {
            List<string[]> entries = new List<string[]>();
            string path = @$"{UnityGame.UserDataPath}\CustomFailText\";

            var linesInFile = File.ReadLines(path + configName + ".txt", new UTF8Encoding(true));
            linesInFile = linesInFile.Where(x => x == "" || x[0] != '#');

            List<string> currentEntry = new List<string>();
            foreach (string line in linesInFile)
            {
                if (line == "")
                {
                    entries.Add(currentEntry.ToArray());
                    currentEntry.Clear();
                }
                else currentEntry.Add(line);
            }
            if (currentEntry.Count != 0) entries.Add(currentEntry.ToArray());
            if (currentEntry.Count == 0)
            {
                _log.Logger.Warn("Config found, but no entries were found! Reverting to default...");
                _config.SelectedConfig = "Default";
            }

            _log.Logger.Info($"Config {configName} contains {entries.Count} entries");
            return entries;
        }

        protected internal const string _defaultConfig =
@"# Custom Fail Text v1.4.0
# by Exomanz, inspired by Arti
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

wasted";
    }
}
