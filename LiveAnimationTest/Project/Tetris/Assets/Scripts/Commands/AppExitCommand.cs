using strange.extensions.command.impl;

namespace Tetris
{
    public class AppExitCommand : Command
    {
        public override void Execute()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif      
            UnityEngine.Application.Quit();
        }
    }
}
