using strange.extensions.command.impl;

namespace Tetris
{
    public class PlayGameCommand : Command
    {
        [Inject]
        public UIManager UIManager { get; private set; }

        [Inject]
        public ScoreManager ScoreManager { get; private set; }

        [Inject]
        public IFigureGenerator FigureGenerator { get; private set; }

        public override void Execute()
        {
            UIManager.GetStartPanel().Close();

            ScoreManager.ResetScore();

            UIManager.GetHUDPanel().Open();

            FigureGenerator.Generate();

        }
    }
}
