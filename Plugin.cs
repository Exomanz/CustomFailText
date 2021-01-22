using BeatSaberMarkupLanguage.Settings;
using BS_Utils.Utilities;
using CustomFailText.Settings;
using IPA;
using Config = IPA.Config.Config;
using IPA.Config.Stores;
using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace CustomFailText
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static Plugin Instance { get; private set; }
        public static IPALogger Log { get; private set; }
        internal string Name => "CustomFailText";
        internal string Version => "v1.1.1";
        public static readonly string[] DEFAULT_TEXT = { "LEVEL", "FAILED" };
        public static List<string[]> allEntries = null;


        [Init]
        public void Init(IPALogger logger, Config conf)
        {
            Instance = this;
            Log = logger;
            PluginConfig.Instance = conf.Generated<PluginConfig>();
        }

        [OnStart]
        public void OnStart()
        {
            Log.Info($"{Name} {Version} Initialized.");
            CheckForDefault();
            BSEvents.lateMenuSceneLoadedFresh += OnMenuSceneLoadedFresh;
            BSEvents.menuSceneLoaded += OnMenuSceneLoaded;
            BSEvents.gameSceneLoaded += OnGameSceneLoaded;
            BSMLSettings.instance.AddSettingsMenu("Custom Fail Text", "CustomFailText.Settings.Views.settings.bsml", SettingsManager.instance);
        }

        [OnExit]
        public void OnExit()
        { }

        void OnMenuSceneLoadedFresh(ScenesTransitionSetupDataSO data)
        {
            Log.Debug("Menu Scene Loaded Fresh");
            ReloadFile();
        }
        void OnMenuSceneLoaded()
        {
            Log.Debug("Menu Scene Loaded");
        }
        void OnGameSceneLoaded()
        {
            if (PluginConfig.Instance.Enabled)
            {
                Log.Debug("Game Scene Loaded");
                new GameObject("_MasterUpdater", new Type[] { typeof(Updater)});
            }
        }

        void ReloadFile()
        {
            allEntries = ReadFromFile();
            Log.Info($"Config {PluginConfig.Instance.SelectedConfig} contains {allEntries.Count} entries.");
        }

        void CheckForDefault()
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
                        byte[] info = new UTF8Encoding(true).GetBytes(DEFAULT_CONFIG.Replace
                            ("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n"));
                        fs.Write(info, 0, info.Length);
                        Log.Notice($"New file {name} was created successfully at {path}!");
                    }
                }
            }
        }

        //Reads lines from selected config with name mathcing mod settings field.
        public static List<string[]> ReadFromFile()
        {
            List<string[]> entriesInFile = new List<string[]>();
            string path = $"{UnityGame.UserDataPath}\\CustomFailText\\";
            string chosenConfig = $"{PluginConfig.Instance.SelectedConfig}.txt";

            var linesInFile = File.ReadLines(path + chosenConfig, new UTF8Encoding(true));
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
            if (currentEntry.Count == 0)
            {
                Log.Warn("Config found, but no entries were found!");
            }

            return entriesInFile;
        }

        public const string DEFAULT_CONFIG =
        #region Default Config
@"# Custom Fail Text v1.1.1
# by Exomanz
#
# Use # for comments!
# Separate entries with empty lines; a random one will be picked each time you fail a song.
# Appears just like in the vanilla game:
LEVEL
FAILED

# TextMeshPro formatting (such as colors) works here!
# Unlike CustomMenuText, each entry is all one piece of text.
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