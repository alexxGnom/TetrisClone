using UnityEngine;

namespace Tetris
{

    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private StartPanelView _startPanel;

        [SerializeField]
        private HUDPanel _hudPanel;

        [SerializeField]
        private EndGamePanelView _endPanel;

        public StartPanelView GetStartPanel()
        {
            return _startPanel;
        }

        public HUDPanel GetHUDPanel()
        {
            return _hudPanel;
        }

        public EndGamePanelView GetEndGamePanel()
        {
            return _endPanel;
        }
    }
}
