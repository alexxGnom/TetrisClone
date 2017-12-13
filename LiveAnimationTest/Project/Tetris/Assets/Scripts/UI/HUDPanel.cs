using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class HUDPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreText;

        public void UpdateScoreText(int score)
        {
            score = Mathf.Max(score, 0);
            _scoreText.text = string.Format("Score : {0}", score);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
