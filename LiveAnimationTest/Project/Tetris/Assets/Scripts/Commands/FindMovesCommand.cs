using UnityEngine;
using strange.extensions.command.impl;

namespace Tetris
{
    public class FindMovesCommand : Command
    {
        [Inject]
        public IField Field { get; private set; }

        [Inject]
        public IFigureGenerator FigureGenerator { get; private set; }

        [Inject]
        public StopGameSignal StopGameSignal { get; private set; }

        public override void Execute()
        {
            if (!IsMoves())
                StopGameSignal.Dispatch();
        }

        private bool IsMoves()
        {
            var figures = FigureGenerator.GetCurrentFigures();

            for (var y = 0; y < Field.Heigh; y++)
            {
                for (var x = 0; x < Field.Width; x++)
                {
                    var cellPos = new Vector2Int(x, y);
                    if (Field.IsCellBusy(cellPos)) continue;

                    foreach (var f in figures)
                    {
                        if (Field.CanSetFigureToField(f, cellPos)) return true;
                    }
                }
            }

            return false;
        }
    }
}
