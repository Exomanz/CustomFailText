﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using BS_Utils.Utilities;
using IPA;
using IPALogger = IPA.Logging.Logger;
using CustomFailText.UI;
using BeatSaberMarkupLanguage.Settings;

namespace CustomFailText
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        #region Definitions
        public CFTRandomizer randText;
        public static Plugin Instance { get; private set; }
        public static IPALogger Log { get; private set; }
        public string path = "\\UserData\\CustomFailText\\CustomFailText.txt";
        public static readonly string[] DEFAULT_TEXT = { "LEVEL", "FAILED" };
        public static List<string[]> allEntries = null;
        #endregion

        [Init]
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Info("CustomFailText Initialized.");
            BSMLSettings.instance.AddSettingsMenu("Custom Fail Text", "CustomFailText.UI.settings.bsml", FailTextUI.instance);
            BSEvents.OnLoad();
            #region Scenes
            BSEvents.menuSceneLoaded += OnMenuSceneLoaded;
            BSEvents.gameSceneLoaded += OnGameSceneLoaded;
            #endregion
            ReloadFile();
        }

        [OnExit]
        public void OnApplicationQuit()
        { }
        private void OnMenuSceneLoaded()
        {
            Log.Info("Menu Scene Loaded. Destroying old randomizer...");
            GameObject.Destroy(GameObject.Find("CFTRandomizer"));
        }
        private void OnGameSceneLoaded()
        {
            Log.Info("Game Scene Loaded. Creating new randomizer and waiting for fail...");
            if (FailTextConfig.FailConfig.GetBool("Custom Fail Text", "enablePlugin", true))
            {
                GameObject.DontDestroyOnLoad(new GameObject("CFTRandomizer", new Type[] { typeof(CFTRandomizer) }));
            }
        }

        public void ReloadFile()
        {
            allEntries = ReadFromFile(path);
        }

        public static List<string[]> ReadFromFile(string path)
        {
            List<string[]> entriesInFile = new List<string[]>();

            string gameDir = Environment.CurrentDirectory;
            gameDir = gameDir.Replace('\\', '/');
            if (File.Exists(gameDir + path))
            {
                var linesInFile = File.ReadLines(gameDir + path, new UTF8Encoding(true));

                linesInFile = linesInFile.Where(s => s == "" || s[0] != '#');

                List<string> currentEntry = new List<string>();
                foreach (string line in linesInFile)
                {
                    if (line =="")
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
            else
            {
                try
                {
                    using (FileStream fs = File.Create(gameDir + path))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(DEFAULT_CONFIG.Replace
                            ("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n"));
                        fs.Write(info, 0, info.Length);
                    }
                }
                catch (Exception)
                {
                    return entriesInFile;
                }
                return ReadFromFile(path);
            }

            return entriesInFile;
        }
        public const string DEFAULT_CONFIG =
#region Default Config
@"# Custom Fail Text v1.0.4
# by Arti
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