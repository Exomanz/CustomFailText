using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace CustomFailText.Services
{
    public class MPUpdater : IInitializable
    {
        //Lights
        TubeBloomPrePassLight _topL;
        TubeBloomPrePassLight _midL;
        TubeBloomPrePassLight _bottomL;

        //Interfaces And Objects
        IMultiplayerLevelEndActionsPublisher _actions;
        IMultiplayerSessionManager _manager;
        PluginConfig _config;
        ResourceHandler _handler;
        TextMeshPro _text;
        bool updated = false;

        [Inject]
        public MPUpdater(PluginConfig config, IMultiplayerLevelEndActionsPublisher actions, IMultiplayerSessionManager manager,
            ResourceHandler handler)
        {
            _config = config;
            _actions = actions;
            _manager = manager;
            _handler = handler;
        }

        public void Initialize()
        {
            CheckSpectating();
            _handler.ReloadFile();
        }

        void CheckSpectating()
        {
            if (_manager.localPlayer.HasState("is_active"))
            {
                Logger.log.Debug("Player is playing.");
                _actions.playerDidFinishEvent += HandlePlayerDidFinish;
            }
            else Logger.log.Debug("Player is spectating. Disabling updater.");
        }

        void HandlePlayerDidFinish(LevelCompletionResults results) 
            { if (results.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed) GetObjects(); }

        async void GetObjects()
        {
            await Task.Run(() => Thread.Sleep(60));
            _topL = GameObject.Find("Neon2").GetComponent<TubeBloomPrePassLight>();
            _midL = GameObject.Find("Neon0").GetComponent<TubeBloomPrePassLight>();
            _bottomL = GameObject.Find("Neon1").GetComponent<TubeBloomPrePassLight>();
            _text = GameObject.Find(nameof(LevelFailedTextEffect)).GetComponentInChildren<TextMeshPro>();

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
