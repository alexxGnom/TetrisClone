using UnityEngine;

namespace Tetris
{
    public interface IFieldBlock
    {
        bool IsActive { get; }

        Vector2Int Adress { get; }

        void Activate();

        void Deactivate();

        void SetColor(Color color);
    }

    public class Block : MonoBehaviour, IFieldBlock
    {

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        #region Interface

        public bool IsActive { get; private set;}

        public Vector2Int Adress { get; set; }

        public void Activate()
        {
            IsActive = true;
            _spriteRenderer.color = Color.white;
        }

        public void Deactivate()
        {
            IsActive = false;
            _spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        #endregion

    }
}
