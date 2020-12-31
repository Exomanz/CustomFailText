using System.Collections;
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

        public IEnumerator Start()
        {
            while (GameObject.Find("GameplayData") == null && GameObject.Find("LevelFailedTextEffect") == null)
            {
                yield return new WaitForEndOfFrame();
            }
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
            if (updated == false)
            {
                updated = true;
                targetText.overflowMode = TextOverflowModes.Overflow;
                targetText.enableWordWrapping = false;

                targetText.text = string.Join("\n", lines);
            }
        }
    }
}