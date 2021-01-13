using CustomFailText.Settings;
using TMPro;
using UnityEngine;

namespace CustomFailText
{
    public class Updater : MonoBehaviour
    {
        private TubeBloomPrePassLight lightMid; //Middle Light
        private TubeBloomPrePassLight lightBottom; //Bottom Light
        private TubeBloomPrePassLight lightTop; //Top Light
        public TextMeshPro targetText;
        public GameEnergyCounter energyCounter;
        public bool updated;

        public void Start()
        {
            targetText = GameObject.Find("LevelFailedTextEffect")?.GetComponent<TextMeshPro>();
            energyCounter = GameObject.Find("GameplayData")?.GetComponent<GameEnergyCounter>();
            updated = false;
        }

        private void GetColors() //Assigns objects in Updater master object
        {
            lightTop = GameObject.Find("Neon2").GetComponent<TubeBloomPrePassLight>();
            lightMid = GameObject.Find("Neon0").GetComponent<TubeBloomPrePassLight>();
            lightBottom = GameObject.Find("Neon1").GetComponent<TubeBloomPrePassLight>();
        }
        
        private void Randomizer()
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

        public void LateUpdate()
        {
            if (energyCounter.energy < 1E-05f)
            {
                GetColors(); //Always throws a NullReferenceException. Fix in progress.
                Randomizer();
            }
        }

        private void Updates(string[] lines) 
        {
            if (updated == false)
            {
                updated = true;

                //Standard (required) updates.
                targetText.overflowMode = 0;
                targetText.enableWordWrapping = false;
                targetText.text = string.Join("\n", lines);

                //Color provider
                if (Configuration.botLightColor != "Red" || Configuration.midLightColor != "Red" || Configuration.topLightColor != "Red")
                {
                    string top = $"{Configuration.topLightColor}";
                    string mid = $"{Configuration.midLightColor}";
                    string bot = $"{Configuration.botLightColor}";

                    if (Configuration.topLightColor != "Red")
                    {
                        switch (top)
                        {
                            case "Blue":
                                lightTop.color = Color.blue;
                                break;
                            case "Cyan":
                                lightTop.color = Color.cyan;
                                break;
                            case "Gray":
                                lightTop.color = Color.gray;
                                break;
                            case "Green":
                                lightTop.color = Color.green;
                                break;
                            case "Magenta":
                                lightTop.color = Color.magenta;
                                break;
                            case "Yellow":
                                lightTop.color = Color.yellow;
                                break;
                            case "White":
                                lightTop.color = Color.white;
                                break;
                        }
                    }
                    if (Configuration.midLightColor != "Red")
                    {
                        switch (mid)
                        {
                            case "Blue":
                                lightMid.color = Color.blue;
                                break;
                            case "Cyan":
                                lightMid.color = Color.cyan;
                                break;
                            case "Gray":
                                lightMid.color = Color.gray;
                                break;
                            case "Green":
                                lightMid.color = Color.green;
                                break;
                            case "Magenta":
                                lightMid.color = Color.magenta;
                                break;
                            case "Yellow":
                                lightMid.color = Color.yellow;
                                break;
                            case "White":
                                lightMid.color = Color.white;
                                break;
                        }
                    }
                    if (Configuration.botLightColor != "Red")
                    {
                        switch (bot)
                        {
                            case "Blue":
                                lightBottom.color = Color.blue;
                                break;
                            case "Cyan":
                                lightBottom.color = Color.cyan;
                                break;
                            case "Gray":
                                lightBottom.color = Color.gray;
                                break;
                            case "Green":
                                lightBottom.color = Color.green;
                                break;
                            case "Magenta":
                                lightBottom.color = Color.magenta;
                                break;
                            case "Yellow":
                                lightBottom.color = Color.yellow;
                                break;
                            case "White":
                                lightBottom.color = Color.white;
                                break;
                        }
                    }
                }

                //Disableable italics text
                if (Configuration.italicText)
                {
                    targetText.fontStyle = 0;
                }
            }
        }
    }
}