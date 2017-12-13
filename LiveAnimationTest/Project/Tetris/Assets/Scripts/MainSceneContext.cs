using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

namespace Tetris
{
    public class MainSceneContext : MVCSContext
    {
        public MainSceneContext(MonoBehaviour contextView) : base(contextView)
        {

        }

        public override void Launch()
        {
            base.Launch();

            var startSignal = injectionBinder.GetInstance<AppStartSignal>();
            startSignal.Dispatch();
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();

            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        private MainSceneContextView _myContextView;

        protected override void mapBindings()
        {
            base.mapBindings();

            _myContextView = (MainSceneContextView)GameObject.FindObjectOfType(typeof(MainSceneContextView));

            //Seff
            injectionBinder.Bind<ScoreManager>().ToSingleton();

            //MonoBehaviours
            injectionBinder.Bind<IField>().ToValue(_myContextView.FieldBehaviour).ToSingleton();
            injectionBinder.Bind<IFigureGenerator>().ToValue(_myContextView.FigureGeneratorBehaviour).ToSingleton();
            injectionBinder.Bind<UIManager>().ToValue(_myContextView.UIManagerBehaviour).ToSingleton();
            injectionBinder.Bind<IAudioManager>().ToValue(_myContextView.AudioManagerBehaviour).ToSingleton();

            // Commands
            commandBinder.Bind<AppStartSignal>().To<AppStartCommand>().Once();
            commandBinder.Bind<AppExitSignal>().To<AppExitCommand>().Once();

            commandBinder.Bind<PlayGameSignal>().To<PlayGameCommand>();
            commandBinder.Bind<StopGameSignal>().To<StopGameCommand>();
            commandBinder.Bind<ScoreChangedSignal>().To<ScoreChangedCommand>();
            commandBinder.Bind<RemoveLineSignal>().To<RemoveLineCommand>();
            commandBinder.Bind<FigureStartDragSignal>().To<FigureStartDragCommand>().Pooled();
            commandBinder.Bind<FigureDraggingSignal>().To<FigureDraggingCommand>().Pooled();
            commandBinder.Bind<FigureDropSignal>().To<FigureDropCommand>().Pooled();
            commandBinder.Bind<FindMovesSignal>().To<FindMovesCommand>();

            //Mediators
            mediationBinder.Bind<InputManagerView>().To<InputManagerMediator>();
            mediationBinder.Bind<StartPanelView>().To<StartPanelMediator>();
            mediationBinder.Bind<EndGamePanelView>().To<EndGamePanelMediator>();
        }

    }
}
