using BeatSaberMarkupLanguage.Settings;
using BS_Utils.Utilities;
using CustomFailText.Settings;
using IPA;
using IPA.Config.Stores;
using IPA.Utilities;
using IPALogger = IPA.Logging.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CustomFailText
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static Plugin Instance { get; private set; }
        public static IPALogger Log { get; private set; }
        internal string Name => "CustomFailText";
        internal string Version => " v1.1.0";
        public static readonly string[] DEFAULT_TEXT = { "LEVEL", "FAILED" };
        public static List<string[]> allEntries = null;
        public static List<string[]> allCustomEntries = null;


        [Init]
        public void Init(IPALogger logger, IPA.Config.Config conf)
        {
            Instance = this;
            Log = logger;
            PluginConfig.Instance = conf.Generated<PluginConfig>();
        }

        [OnStart]
        public void OnStart()
        {
            Log.Info($"{Name + Version} Initialized.");
            CheckForDefault();
            BSEvents.lateMenuSceneLoadedFresh += OnMenuSceneLoadedFresh;
            BSEvents.menuSceneLoaded += OnMenuSceneLoaded;
            BSEvents.gameSceneLoaded += OnGameSceneLoaded;
            BSMLSettings.instance.AddSettingsMenu("Custom Fail Text", "CustomFailText.Settings.Views.settings.bsml", SettingsManager.instance);
        }

        [OnExit]
        public void OnExit()
        { }

        private void OnMenuSceneLoadedFresh(ScenesTransitionSetupDataSO data)
        {
            Log.Info("Menu Scene Loaded Fresh");
            ReloadFile();
        }
        private void OnMenuSceneLoaded()
        {
            Log.Info("Menu Scene Loaded");
        }
        private void OnGameSceneLoaded()
        {
            if (PluginConfig.Instance.Enabled)
            {
                Log.Info("Game Scene Loaded");
                new GameObject("_MasterUpdater", new Type[] { typeof(Updater)});
            }
        }

        public void ReloadFile()
        {
            if (PluginConfig.Instance.SelectedConfig == "Default")
            {
                allEntries = ReadFromFile();
                Log.Info($"Config {PluginConfig.Instance.SelectedConfig} contains {allEntries.Count} entries.");
            }
            else
            { 
                allCustomEntries = ReadFromCustomFile();
                Log.Info($"Config {PluginConfig.Instance.SelectedConfig} contains {allCustomEntries.Count} entries.");
            }
        }

        private void CheckForDefault()
        {
            string path = $"{UnityGame.InstallPath}\\UserData\\CustomFailText\\";
            string name = "Default.txt";

            if (File.Exists(path + name))
            {
                return;
            }
            else
            { 
                Log.Warn($"No file {name} found at {path}. Making one now.");
                {
                    Directory.CreateDirectory(path);
                    using (FileStream fs = File.Create(path + name))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(DEFAULT_CONFIG.Replace
                            ("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n"));
                        fs.Write(info, 0, info.Length);
                        Log.Notice($"New file {name} was created successfully at {path}!");
                    }
                }
            }
        }

        //Reads lines from selected config with name mathcing mod settings field.
        public static List<string[]> ReadFromCustomFile()
        {
            PluginConfig settings = new PluginConfig();
            List<string[]> entriesInCustomFile = new List<string[]>();
            string path = $"{UnityGame.UserDataPath}\\CustomFailText\\";
            string chosenConfig = $"{settings.SelectedConfig}.txt";

            if (settings.SelectedConfig != "Default")
            {
                var linesInFile = File.ReadLines(path + chosenConfig, new UTF8Encoding(true));
                linesInFile = linesInFile.Where(s => s == "" || s[0] != '#');

                List<string> currentEntry = new List<string>();
                foreach (string line in linesInFile)
                {
                    if (line == "")
                    {
                        entriesInCustomFile.Add(currentEntry.ToArray());
                        currentEntry.Clear();
                    }
                    else
                    {
                        currentEntry.Add(line);
                    }
                }
                if (currentEntry.Count != 0)
                {
                    entriesInCustomFile.Add(currentEntry.ToArray());
                }
                if (currentEntry.Count == 0)
                {
                    Log.Warn("Custom config found, but no entries were found!");
                }
            }
            return entriesInCustomFile;
        }

        //Reads lines only from "Default.txt" and populates entries.
        public static List<string[]> ReadFromFile()
        {
            List<string[]> entriesInFile = new List<string[]>();
            string path = $"{UnityGame.InstallPath}\\UserData\\CustomFailText\\";
            string fileName = "Default.txt";

            if (File.Exists(path + fileName))
            {
                var linesInFile = File.ReadLines(path + fileName, new UTF8Encoding(true));
                linesInFile = linesInFile.Where(s => s == "" || s[0] != '#');

                List<string> currentEntry = new List<string>();
                foreach (string line in linesInFile)
                {
                    if (line == "")
                    {
                        entriesInFile.Add(currentEntry.ToArray());
                        currentEntry.Clear();
                    }
                    else
                    {
                        currentEntry.Add(line);
                    }
                }
                if (currentEntry.Count != 0)
                {
                    entriesInFile.Add(currentEntry.ToArray());
                }
                if (entriesInFile.Count == 0)
                {
                    Log.Warn("File found, but it contained no entries!");
                }
            }
            return entriesInFile;
        }
        public const string DEFAULT_CONFIG =
        #region Default Config
@"# Custom Fail Text v1.1.0
# by Exomanz
#
# Use # for comments!
# Separate entries with empty lines; a random one will be picked each time you fail a song.
# Appears just like in the vanilla game:
LEVEL
FAILED

# TextMeshPro formatting (such as colors) works here!
# Unlike CustomMenuText, each entry is all one piece of text, so remember to close your tags at the end of each line!
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
        #endregion
    }
}