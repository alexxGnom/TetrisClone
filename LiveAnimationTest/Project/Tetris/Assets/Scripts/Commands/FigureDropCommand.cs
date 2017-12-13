using strange.extensions.command.impl;

namespace Tetris
{
    public class FigureDropCommand : Command
    {
        [Inject]
        public IField Field { get; private set; }

        [Inject]
        public IDraggableFigure Figure { get; private set; }

        [Inject]
        public IFigureGenerator FigureGenerator { get; private set; }

        [Inject]
        public IAudioManager AudioManager { get; private set; }


        public override void Execute()
        {
            if (Figure == null) return;

            Figure.OnDrop();

            if (Field.SetFigure(Figure))
            {
                AudioManager.PlayOnce("Drop");
                FigureGenerator.UseFigure(Figure);
                Figure.Despawn();
            }
            else
            {
                Figure.ReturnToStartPosition();
            }
        }
    }
}
