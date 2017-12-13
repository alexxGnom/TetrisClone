using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine.UI;

namespace Tetris
{
    public class StartPanelView : View
    {
        [SerializeField]
        private Button StartBttn;

        public Signal StartPressSignal { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            StartPressSignal = new Signal();

            StartBttn.onClick.AddListener(() => StartPressSignal.Dispatch());
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
