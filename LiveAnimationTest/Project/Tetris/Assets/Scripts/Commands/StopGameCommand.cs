using strange.extensions.command.impl;

namespace Tetris
{
    public class StopGameCommand : Command
    {
        [Inject]
        public IField Field { get; private set; }

        [Inject]
        public IFigureGenerator FigureGenerator { get; private set; }

        [Inject]
        public UIManager UiManager { get; private set; }

        public override void Execute()
        {
            UiManager.GetHUDPanel().Close();
            UiManager.GetEndGamePanel().Open();
        }
    }
}
