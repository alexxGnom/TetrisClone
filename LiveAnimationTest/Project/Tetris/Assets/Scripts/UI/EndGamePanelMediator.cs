using strange.extensions.mediation.impl;

namespace Tetris
{
    public class EndGamePanelMediator : Mediator
    {
        [Inject]
        public EndGamePanelView View { get; private set; }

        [Inject]
        public UIManager UiManager { get; private set; }

        [Inject]
        public ScoreManager ScoreManager { get; private set; }

        [Inject]
        public IAudioManager AudioManager { get; private set; }

        public override void OnRegister()
        {
            View.OnOpenSignal.AddListener(OnOpen);
            View.OnPressOkSignal.AddListener(OnEndClick);
        }

        public override void OnRemove()
        {
            View.OnOpenSignal.RemoveListener(OnOpen);
            View.OnPressOkSignal.RemoveListener(OnEndClick);
        }

        private void OnOpen()
        {
            View.UpdateScoreText(ScoreManager.Score);
        }

        private void OnEndClick()
        {
            AudioManager.PlayOnce("Click");
            View.Close();
            UiManager.GetStartPanel().Open();
        }
    }
}
