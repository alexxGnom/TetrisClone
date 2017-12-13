using strange.extensions.command.impl;
using UnityEngine;

namespace Tetris
{
    public class AppStartCommand : Command
    {
        public override void Execute()
        {
#if UNITY_EDITOR
            Debug.Log("App Started");
#endif
        }
    }
}
