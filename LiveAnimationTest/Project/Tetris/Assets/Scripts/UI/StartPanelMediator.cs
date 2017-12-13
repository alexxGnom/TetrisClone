using strange.extensions.mediation.impl;

namespace Tetris
{
    public class StartPanelMediator : Mediator
    {

        [Inject]
        public StartPanelView View { get; private set; }

        [Inject]
        public PlayGameSignal PlayGameSignal { get; private set; }

        [Inject]
        public IAudioManager AudioManager { get; private set; }

        public override void OnRegister()
        {
            View.StartPressSignal.AddListener(OnStartClick);
        }

        public override void OnRemove()
        {
            View.StartPressSignal.RemoveListener(OnStartClick);
        }

        private void OnStartClick()
        {
            AudioManager.PlayOnce("Click");
            PlayGameSignal.Dispatch();
        }
    }
}
