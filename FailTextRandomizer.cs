using System.Collections;
using TMPro;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace CustomFailText
{
    public class FailTextRandomizer : MonoBehaviour
    {
        #region Definitions
        public static IPALogger Log { get; private set; }
        public static FailTextRandomizer randText;
        public TextMeshPro targetText;
        public GameEnergyCounter energyCounter;
        public bool latch;
        #endregion

        public IEnumerator Start()
        {
            while (GameObject.Find("GameplayData") == null && GameObject.Find("LevelFailedTextEffect") == null)
            {
                yield return new WaitForEndOfFrame();
            }
            targetText = GameObject.Find("LevelFailedTextEffect")?.GetComponent<TextMeshPro>();
            energyCounter = GameObject.Find("GameplayData")?.GetComponent<GameEnergyCounter>();
        }

        public void LateUpdate()
        {
            if (targetText == null || energyCounter == null)
            {
                Start();
            }
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
            targetText.overflowMode = TextOverflowModes.Overflow;
            targetText.enableWordWrapping = false;

            targetText.text = string.Join("\n", lines);
        }
    }
}