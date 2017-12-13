using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace Tetris
{
    public class EndGamePanelView : View
    {
        [SerializeField]
        private float _delayShowScore;

        [SerializeField]
        private Text _endText;

        [SerializeField]
        private Button _okBttn;

        public void Open()
        {
            gameObject.SetActive(true);

            _endText.text = "End Game!";
            StartCoroutine(ScoreUpdateRoutine());
        }

        private IEnumerator ScoreUpdateRoutine()
        {
            yield return new WaitForSeconds(_delayShowScore);
            OnOpenSignal.Dispatch();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public Signal OnOpenSignal { get; private set; }
        public Signal OnPressOkSignal { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            OnPressOkSignal = new Signal();
            OnOpenSignal = new Signal();

            _okBttn.onClick.AddListener(() => OnPressOkSignal.Dispatch());
        }

        public void UpdateScoreText(int score)
        {
            score = Mathf.Max(score, 0);
            _endText.text = string.Format("Score : {0}", score);
        }

    }
}
