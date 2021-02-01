using CustomFailText.Settings;
using TMPro;
using UnityEngine;

namespace CustomFailText
{
    public class Updater : MonoBehaviour
    {
        TubeBloomPrePassLight lightTop; //Top Light
        TubeBloomPrePassLight lightMid; //Middle Light
        TubeBloomPrePassLight lightBottom; //Bottom Light
        TextMeshPro targetText;
        GameEnergyCounter energyCounter;
        bool updated = false;

        void Start()
        {
            targetText = GameObject.Find("LevelFailedTextEffect")?.GetComponent<TextMeshPro>();
            energyCounter = GameObject.Find("GameplayData")?.GetComponent<GameEnergyCounter>();
        }

        void LateUpdate()
        {
            if (energyCounter.energy < 1E-05f) PreUpdates();
        }
        
        void PreUpdates()
        {
            lightTop = GameObject.Find("Neon2")?.GetComponent<TubeBloomPrePassLight>();
            lightMid = GameObject.Find("Neon0")?.GetComponent<TubeBloomPrePassLight>();
            lightBottom = GameObject.Find("Neon1")?.GetComponent<TubeBloomPrePassLight>();

            if (lightTop != null && lightMid != null && lightBottom != null) Randomizer();
            else return;
        }

        void Randomizer()
        {
            System.Random r = new System.Random();
            int entryPicked = r.Next(Plugin.allEntries.Count);
            Updates(Plugin.allEntries[entryPicked]);
        }

        void Updates(string[] lines) 
        {
            if (!updated)
            {
                //Standard Updates
                targetText.overflowMode = TextOverflowModes.Overflow;
                targetText.enableWordWrapping = false;
                targetText.text = string.Join("\n", lines);
                updated = true;

                //Disable Italics Text
                if (PluginConfig.Instance.DisableItalics) targetText.fontStyle = FontStyles.Normal;

                //Color Manager
                if (PluginConfig.Instance.EnableLights)
                {
                    lightTop.color = PluginConfig.Instance.topColor;
                    lightMid.color = PluginConfig.Instance.midColor;
                    lightBottom.color = PluginConfig.Instance.bottomColor;
                }
            }
        }
    }
}