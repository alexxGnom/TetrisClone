using strange.extensions.command.impl;

namespace Tetris
{
    public class RemoveLineCommand : Command
    {
        [Inject]
        public ScoreManager ScoreManager { get; private set; }

        [Inject]
        public int Score { get; private set; }

        public override void Execute()
        {
            ScoreManager.AddScore(Score);
        }
    }
}
