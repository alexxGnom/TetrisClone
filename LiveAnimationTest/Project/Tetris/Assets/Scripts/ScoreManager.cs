namespace Tetris
{
    public class ScoreManager
    {
        #region Private fields

        private int _score;

        #endregion

        #region Injects

        [Inject]
        public ScoreChangedSignal ScoreChangedSignal { get; private set; }

        [Inject]
        public StopGameSignal StopGameSignal { get; private set; }

        #endregion

        #region Interface

        public int Score
        {
            get
            {
                return _score;
            }

            set
            {
                _score = value;
                ScoreChangedSignal.Dispatch(Score);
            }
        }

        public void AddScore(int score)
        {
            if (score > 0)
                Score += score;
        }

        public void ResetScore()
        {
            Score = 0;
        }

        #endregion

    }
}
