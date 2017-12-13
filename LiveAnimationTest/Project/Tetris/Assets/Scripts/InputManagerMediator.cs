using strange.extensions.mediation.impl;

namespace Tetris
{
    public class InputManagerMediator : Mediator
    {
        [Inject]
        public InputManagerView View { get; private set; }

        [Inject]
        public FigureStartDragSignal FigureStartDragSignal { get; private set; }

        [Inject]
        public FigureDraggingSignal FigureDraggingSignal { get; private set; }

        [Inject]
        public FigureDropSignal FigureDropSignal { get; private set; }

        [Inject]
        public AppExitSignal AppExitSignal { get; private set; }

        public override void OnRegister()
        {
            View.StartDragSignal.AddListener(OnFigureStartDrag);
            View.DraggingSignal.AddListener(OnFigureDragged);
            View.DropSignal.AddListener(OnFigureDroped);

            View.EscapeSignal.AddListener(OnBackButton);
        }

        public override void OnRemove()
        {
            View.StartDragSignal.RemoveListener(OnFigureStartDrag);
            View.DraggingSignal.RemoveListener(OnFigureDragged);
            View.DropSignal.RemoveListener(OnFigureDroped);

            View.EscapeSignal.RemoveListener(OnBackButton);
        }

        private void OnFigureStartDrag()
        {
            FigureStartDragSignal.Dispatch(View.Figure);
        }

        private void OnFigureDragged()
        {
            FigureDraggingSignal.Dispatch(View.Figure);
        }

        private void OnFigureDroped()
        {
            FigureDropSignal.Dispatch(View.Figure);
        }

        private void OnBackButton()
        {
            AppExitSignal.Dispatch();
        }
    }
}
