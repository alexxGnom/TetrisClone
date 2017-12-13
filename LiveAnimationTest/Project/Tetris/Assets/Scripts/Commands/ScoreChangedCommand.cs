using strange.extensions.command.impl;

namespace Tetris
{
    public class ScoreChangedCommand : Command
    {
        [Inject]
        public int Score { get; private set; }

        [Inject]
        public UIManager UiManager { get; private set; }

        public override void Execute()
        {
            UiManager.GetHUDPanel().UpdateScoreText(Score);
        }
    }
}
