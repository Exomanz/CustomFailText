using CustomFailText.Settings;
using TMPro;
using UnityEngine;

namespace CustomFailText
{
    public class Updater : MonoBehaviour
    {
        private TubeBloomPrePassLight lightTop; //Top Light
        private TubeBloomPrePassLight lightMid; //Middle Light
        private TubeBloomPrePassLight lightBottom; //Bottom Light
        public TextMeshPro targetText;
        public GameEnergyCounter energyCounter;
        public bool updated;

        public void Start()
        {
            targetText = GameObject.Find("LevelFailedTextEffect")?.GetComponent<TextMeshPro>();
            energyCounter = GameObject.Find("GameplayData")?.GetComponent<GameEnergyCounter>();
            updated = false;
        }

        public void LateUpdate()
        {
            if (energyCounter.energy < 1E-05f) PreUpdates();
        }
        
        private void PreUpdates()
        {
            lightTop = GameObject.Find("Neon2")?.GetComponent<TubeBloomPrePassLight>();
            lightMid = GameObject.Find("Neon0")?.GetComponent<TubeBloomPrePassLight>();
            lightBottom = GameObject.Find("Neon1")?.GetComponent<TubeBloomPrePassLight>();

            if (lightTop != null && lightMid != null && lightBottom != null) Randomize();
            else return;
        }

        private void Randomize()
        {
            System.Random r = new System.Random();
            if (PluginConfig.Instance.SelectedConfig == "Default")
            {
                int entryPicked = r.Next(Plugin.allEntries.Count);
                Updates(Plugin.allEntries[entryPicked]);
            }
            else
            {
                int entryPicked = r.Next(Plugin.allCustomEntries.Count);
                Updates(Plugin.allCustomEntries[entryPicked]);
            }
        }

        private void Updates(string[] lines) 
        {
            if (updated == false)
            {
                //Standard Updates
                targetText.overflowMode = TextOverflowModes.Overflow;
                targetText.enableWordWrapping = false;
                targetText.text = string.Join("\n", lines);
                updated = true;

                //Disable Italics Text
                if (PluginConfig.Instance.DisableItalics) targetText.fontStyle = FontStyles.Normal;

                //Color Manager
                lightBottom.color = PluginConfig.Instance.topColor;
                lightMid.color = PluginConfig.Instance.midColor;
                lightTop.color = PluginConfig.Instance.bottomColor;
            }
        }
    }
}