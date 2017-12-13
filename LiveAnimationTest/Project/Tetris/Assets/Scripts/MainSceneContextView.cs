using UnityEngine;
using strange.extensions.context.impl;

namespace Tetris
{
    public class MainSceneContextView : ContextView
    {
        [SerializeField]
        private Field _fieldBehaviour;

        [SerializeField]
        private UIManager _uiManager;

        [SerializeField]
        private FigureGenerator _figureGenerator;

        [SerializeField]
        private SimplyAudioManager _audioManager;

        public Field FieldBehaviour { get { return _fieldBehaviour; } }

        public UIManager UIManagerBehaviour { get { return _uiManager; } }

        public FigureGenerator FigureGeneratorBehaviour { get { return _figureGenerator; } }

        public SimplyAudioManager AudioManagerBehaviour { get { return _audioManager; } }

        void Awake()
        {
            context = new MainSceneContext(this);
        }
    }
}
