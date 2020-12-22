﻿using CustomFailText.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace CustomFailText
{
    public class CFTRandomizer : MonoBehaviour
    {
        #region Definitions
        public static CFTRandomizer randText;
        public TextMeshPro targetText;
        public GameEnergyCounter energyCounter;
        public bool latch;
        public bool updateHappened;
        #endregion

        public IEnumerator Start()
        {
            while (GameObject.Find("GameplayData") == null && GameObject.Find("LevelFailedTextEffect") == null)
            {
                yield return new WaitForEndOfFrame();
            }
            targetText = GameObject.Find("LevelFailedTextEffect")?.GetComponent<TextMeshPro>();
            energyCounter = GameObject.Find("GameplayData")?.GetComponent<GameEnergyCounter>();
            updateHappened = false;
        }

        public void LateUpdate()
        { 
            if (energyCounter.energy < 1E-05f)
            {
                if (latch) return;
                latch = true;

                if (Plugin.allEntries.Count == 0)
                {
                    TextUpdates(Plugin.DEFAULT_TEXT);
                }
                else
                {
                    System.Random r = new System.Random();
                    int entryPicked = r.Next(Plugin.allEntries.Count);

                    TextUpdates(Plugin.allEntries[entryPicked]);
                }
            }
            else
            {
                latch = false;
            }
        }

        private void TextUpdates(string[] lines)
        {
            if (updateHappened == false)
            {
                updateHappened = true;
                targetText.overflowMode = 0;
                targetText.enableWordWrapping = false;

                targetText.text = string.Join("\n", lines);
                //Disableable Italics Text
                if (FailTextConfig.FailConfig.GetBool("Custom Fail Text", "italicText", false))
                {
                    targetText.fontStyle = 0;
                }
            }
        }
    }
}