using strange.extensions.command.impl;

namespace Tetris
{
    public class FigureStartDragCommand : Command
    {
        [Inject]
        public IDraggableFigure Figure { get; private set; }

        public override void Execute()
        {
            if (Figure == null) return;

            Figure.OnStartDrag();
        }
    }
}
