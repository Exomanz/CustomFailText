using CustomFailText.Settings;
using TMPro;
using UnityEngine;

namespace CustomFailText
{
    public class FailTextRandomizer : MonoBehaviour
    {
        #region Definitions
        public TextMeshPro targetText;
        public GameEnergyCounter energyCounter;
        public bool latch;
        public bool updated;
        #endregion

        public void Start()
        {
            targetText = GameObject.Find("LevelFailedTextEffect")?.GetComponent<TextMeshPro>();
            energyCounter = GameObject.Find("GameplayData")?.GetComponent<GameEnergyCounter>();
            updated = false;
        }

        public void LateUpdate()
        {
            if (energyCounter.energy < 1E-05f)
            {
                if (latch) return;
                latch = true;
                Randomize();
            }
            else
            {
                latch = false;
            }
        }
        
        private void Randomize()
        {
            System.Random r = new System.Random();
            if (Configuration.selectedConfig == "Default")
            {
                if (Plugin.allEntries.Count == 0)
                {
                    Updates(Plugin.DEFAULT_TEXT);
                }
                else
                {
                    int entryPicked = r.Next(Plugin.allEntries.Count);
                    Updates(Plugin.allEntries[entryPicked]);
                }
            }
            else
            {
                if (Plugin.allCustomEntries.Count == 0) //If for some reason your config file has no entries... not sure why it would be empty--you made it.
                {
                    Updates(Plugin.DEFAULT_TEXT);
                }
                else
                {
                    int entryPicked = r.Next(Plugin.allCustomEntries.Count);
                    Updates(Plugin.allCustomEntries[entryPicked]);
                }
            }
        }

        private void Updates(string[] lines) 
        {
            if (updated == false)
            {
                //Prevents multiple updates.
                updated = true;

                //Standard (required) updates.
                targetText.overflowMode = TextOverflowModes.Overflow;
                targetText.enableWordWrapping = false;
                targetText.text = string.Join("\n", lines);

                //Disableable italics text
                if (Configuration.italicText == true)
                {
                    targetText.fontStyle = FontStyles.Normal;
                }
            }
        }
    }
}