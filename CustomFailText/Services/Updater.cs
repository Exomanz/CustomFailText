using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace CustomFailText.Services
{
    public class Updater : IInitializable, IDisposable
    {
        TubeBloomPrePassLight _topL;
        TubeBloomPrePassLight _midL;
        TubeBloomPrePassLight _bottomL;

        PluginConfig _config;
        ILevelEndActions _actions;
        TextMeshPro _text;
        bool updated = false;

        [Inject]
        public Updater(PluginConfig config, ILevelEndActions actions)
        {
            _config = config;
            _actions = actions;
        }

        public void Initialize()
        {
            _text = GameObject.Find(nameof(LevelFailedTextEffect)).GetComponent<TextMeshPro>();
            _actions.levelFailedEvent += Lights;
        }

        async void Lights()
        {
            await Task.Run(() => Thread.Sleep(100));
            _topL = GameObject.Find("Neon2").GetComponent<TubeBloomPrePassLight>();
            _midL = GameObject.Find("Neon0").GetComponent<TubeBloomPrePassLight>();
            _bottomL = GameObject.Find("Neon1").GetComponent<TubeBloomPrePassLight>();

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

        public void Dispose() => _actions.levelFailedEvent -= Lights;
    }
}