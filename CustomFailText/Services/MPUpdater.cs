using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CustomFailText.Services
{
    public class MPUpdater : IInitializable, IDisposable
    {
        TubeBloomPrePassLight _topL;
        TubeBloomPrePassLight _midL;
        TubeBloomPrePassLight _bottomL;

        IMultiplayerLevelEndActionsPublisher _actions;
        PluginConfig _config;
        TextMeshPro _text;
        bool updated = false;

        [Inject]
        public MPUpdater(PluginConfig config, IMultiplayerLevelEndActionsPublisher actions)
        {
            _config = config;
            _actions = actions;
        }

        public void Initialize() => _actions.playerDidFinishEvent += HandlePlayerDidFinish;

        void HandlePlayerDidFinish(LevelCompletionResults results) 
            { if (results.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed) GetObjects(); }

        async void GetObjects()
        {
            await Task.Run(() => Thread.Sleep(100));
            _topL = GameObject.Find("Neon2").GetComponent<TubeBloomPrePassLight>();
            _midL = GameObject.Find("Neon0").GetComponent<TubeBloomPrePassLight>();
            _bottomL = GameObject.Find("Neon1").GetComponent<TubeBloomPrePassLight>();
            _text = GameObject.Find(nameof(LevelFailedTextEffect)).GetComponent<TextMeshPro>();

            if (_topL != null && _midL != null && _bottomL != null && _text != null) Randomizer();
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

        public void Dispose() { _actions.playerDidFinishEvent -= HandlePlayerDidFinish; }
    }
}
