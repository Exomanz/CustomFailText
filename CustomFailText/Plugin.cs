using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BS_Utils.Utilities;
using BeatSaberMarkupLanguage.Settings;
using CustomFailText.Settings;
using IPA;
using IPALogger = IPA.Logging.Logger;
using IPA.Utilities;
using UnityEngine;

namespace CustomFailText
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static Plugin Instance { get; private set; }
        public static IPALogger Log { get; private set; }
        internal string Name => "CustomFailText";
        internal string Version => " v1.0.2";
        public static readonly string[] DEFAULT_TEXT = { "LEVEL", "FAILED" };
        public static List<string[]> allEntries = null;
        public static List<string[]> allCustomEntries = null;


        [Init]
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
        }

        [OnStart]
        public void OnStart()
        {
            Log.Info($"{Name + Version} Initialized.");
            CheckForDefault();
            BSEvents.OnLoad();
            BSEvents.lateMenuSceneLoadedFresh += OnMenuSceneLoadedFresh;
            BSEvents.menuSceneLoaded += OnMenuSceneLoaded;
            BSEvents.gameSceneLoaded += OnGameSceneLoaded;
            BSMLSettings.instance.AddSettingsMenu("CustomFailText", "CustomFailText.Settings.Views.settings.bsml", SettingsManager.instance);
        }

        [OnExit]
        public void OnExit()
        { }
        private void OnMenuSceneLoadedFresh(ScenesTransitionSetupDataSO data)
        {
            Log.Info("Menu Scene Loaded Fresh!");
            ReloadFile();
        }
        private void OnMenuSceneLoaded()
        {
            Log.Info("Menu Scene Loaded. Destroying old randomizer.");
            GameObject.Destroy(GameObject.Find("FailTextRandomizer"));
        }
        private void OnGameSceneLoaded()
        {
            if (Configuration.config.GetBool("Custom Fail Text", "enablePlugin") == true)
            {
                Log.Info("Game Scene Loaded. Creating new randomizer.");
                new GameObject("FailTextRandomizer", new Type[] { typeof(FailTextRandomizer)});
            }
        }
        public void ReloadFile()
        {
            if (Configuration.selectedConfig == "Default")
            {
                string path = $"{UnityGame.InstallPath}\\UserData\\CustomFailText\\Default.txt";
                if (File.Exists(path))
                {
                    allEntries = ReadFromFile();
                    Log.Info($"Config {Configuration.selectedConfig} contains {allEntries.Count} entries.");
                }
            }
            else
            {
                allCustomEntries = ReadFromCustomFile();
                Log.Info($"Config {Configuration.selectedConfig} contains {allCustomEntries.Count} entries.");
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
                    Directory.CreateDirectory($"{UnityGame.InstallPath}\\UserData\\CustomFailText");
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
            List<string[]> entriesInCustomFile = new List<string[]>();
            string path = $"{UnityGame.UserDataPath}\\CustomFailText\\";
            string chosenConfig = $"{Configuration.selectedConfig}.txt";

            if (Configuration.selectedConfig != "Default")
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
@"# Custom Fail Text v1.0.2
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