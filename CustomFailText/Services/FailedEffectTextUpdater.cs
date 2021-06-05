using SiraUtil.Tools;
using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace CustomFailText.Services
{
    public class FailedEffectTextUpdater : IInitializable, IDisposable
    {
        PluginConfig _config;
        TubeBloomPrePassLight[] _lights;
        TextMeshPro _text;

        IGameEnergyCounter _energyCounter;
        LevelFailedTextEffect _effect;
        GameplayModifiers _mods;
        ListBuilder _builder;
        SiraLog _log;

        public FailedEffectTextUpdater(LevelFailedTextEffect effect, IGameEnergyCounter energyCounter, PluginConfig config
            , GameplayModifiers mods, ListBuilder builder, SiraLog log)
        {
            _config = config;
            _effect = effect;
            _energyCounter = energyCounter;
            _builder = builder;
            _mods = mods;
            _log = log;
        }

        public void Initialize()
        {
            if (_mods.noFailOn0Energy || !_config.Enabled) return;
            _energyCounter.gameEnergyDidReach0Event += LevelWasFailedEvent;
        }

        internal async void LevelWasFailedEvent()
        {
            await Task.Run(() => Thread.Sleep(50));
            _lights = _effect.GetComponentsInChildren<TubeBloomPrePassLight>();
            _text = _effect.GetComponentInChildren<TextMeshPro>();

            System.Random r = new System.Random();
            int entryPicked = r.Next(_builder.allEntries.Count);
            EffectUpdater(_builder.allEntries[entryPicked]);
        }

        public void EffectUpdater(string[] lines)
        {
            bool updated = false;
            if (!updated)
            {
                //Standard Updates
                _text.enableWordWrapping = false;
                _text.richText = true;
                _text.text = string.Join("\n", lines);

                //Effect and Text Fixes
                _effect.transform.position = new Vector3(0f, 1.5f, 11.19f);
                _effect.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
                _text.transform.localPosition = Vector3.zero;
                _text.lineSpacing = -40f;
                _text.fontSize = 10f;

                //Light Fixes
                _lights[1].transform.localPosition = new Vector3(0f, -0.75f, 0f);
                _lights[2].transform.localPosition = new Vector3(0f, 0.75f, 0f);

                if (_config.DisableItalics) _text.fontStyle = FontStyles.Normal;

                if (_config.LightColorer)
                {
                    _lights[0].color = _config.MiddleColor;
                    _lights[1].color = _config.BottomColor;
                    _lights[2].color = _config.TopColor;
                }

                updated = true;
            }
        }

        public void Dispose() => _energyCounter.gameEnergyDidReach0Event -= LevelWasFailedEvent;
    }
}
