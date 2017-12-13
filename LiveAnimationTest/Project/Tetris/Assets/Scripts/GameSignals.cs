using strange.extensions.signal.impl;

namespace Tetris
{

    public class AppStartSignal : Signal { }

    public class AppExitSignal : Signal { }

    public class FigureDropSignal : Signal<IDraggableFigure> { }

    public class FigureStartDragSignal : Signal<IDraggableFigure> { }

    public class FigureDraggingSignal : Signal<IDraggableFigure> { }

    public class ScoreChangedSignal : Signal<int> { }

    public class RemoveLineSignal : Signal<int> { }

    public class FindMovesSignal : Signal { }

    public class PlayGameSignal : Signal { }

    public class StopGameSignal : Signal { }

}

