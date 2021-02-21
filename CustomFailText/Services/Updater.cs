using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace CustomFailText.Services
{
    public class Updater : IInitializable, IDisposable
    {
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
            _actions.levelFailedEvent += Randomizer;
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
            }
        }

        public void Dispose() => _actions.levelFailedEvent -= Randomizer;
    }
}