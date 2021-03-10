using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace CustomFailText.Services
{
    public class SPUpdater : IInitializable
    {
        TubeBloomPrePassLight _topL;
        TubeBloomPrePassLight _midL;
        TubeBloomPrePassLight _bottomL;

        ILevelEndActions _actions;
        PluginConfig _config;
        ResourceHandler _handler;
        TextMeshPro _text;
        bool updated = false;

        [Inject]
        public SPUpdater(PluginConfig config, ILevelEndActions actions, ResourceHandler handler)
        {
            _config = config;
            _actions = actions;
            _handler = handler;
        }

        public void Initialize()
        {
            _handler.ReloadFile();
            _actions.levelFailedEvent += Lights;
        }

        async void Lights()
        {
            await Task.Run(() => Thread.Sleep(50));
            _topL = GameObject.Find("Neon2").GetComponent<TubeBloomPrePassLight>();
            _midL = GameObject.Find("Neon0").GetComponent<TubeBloomPrePassLight>();
            _bottomL = GameObject.Find("Neon1").GetComponent<TubeBloomPrePassLight>();
            _text = GameObject.Find(nameof(LevelFailedTextEffect)).GetComponentInChildren<TextMeshPro>();

            if (_topL != null && _midL != null && _bottomL != null) Randomizer();
        }

        void Randomizer()
        {
            System.Random r = new System.Random();
            int entryPicked = r.Next(ResourceHandler.allEntries.Count);
            Updates(ResourceHandler.allEntries[entryPicked]);
        }

        void Updates(string[] lines)
        {
            if (!updated)
            {
                //Standard
                _text.overflowMode = TextOverflowModes.Overflow;
                _text.enableWordWrapping = false;
                _text.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                _text.lineSpacing = -40f;
                _text.richText = true;
                _text.text = string.Join("\n", lines);
                updated = true;

                //Italics
                if (_config.DisableItalics) _text.fontStyle = FontStyles.Normal;

                //Colors
                if (_config.EnableLights)
                {
                    _topL.color = _config.topColor;
                    _midL.color = _config.midColor;
                    _bottomL.color = _config.bottomColor;
                }
            }
        }
    }
}