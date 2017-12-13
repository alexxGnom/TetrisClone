using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace Tetris
{
    public interface IFigure
    {
        Vector3 Position { get; }

        List<Vector2Int> Mask { get; }

        Color BrickColor { get; }

        void SetToPosition(Vector3 pos);

        void Rotate(int count);

        void ReturnToStartPosition();

        void SetStartScale();

        void Spawn(Vector3 position, Transform parent);

        void Despawn();
    }

    public interface IDraggable
    {
        void OnStartDrag();

        void OnDragged();

        void OnDrop();
    }

    public interface IDraggableFigure : IDraggable, IFigure { }

    public class FigureView : View, IDraggableFigure
    {
        public const string BRICK_TAG = "Block";

        #region Unity properties

        [SerializeField]
        private Transform _dragPivot;

        [SerializeField]
        private float _startScale = 0.75f;

        #endregion

        #region Private fields

        private Vector3 _dragOffset;

        private Vector3 _startPos;

        private List<Vector2Int> _mask;

        private SpriteRenderer _spriteRenderer;

        private Color _color;

        #endregion

        #region Interface IFigure

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public List<Vector2Int> Mask
        {
            get { return _mask; }
        }

        public Color BrickColor
        {
            get { return _color; }
        }

        public void SetToPosition(Vector3 pos)
        {
            transform.position = pos + Vector3.Scale(_dragOffset, transform.localScale);
        }

        public void Rotate(int count)
        {
            count = count % 4;
            transform.rotation *= Quaternion.Euler(0f, 0f, 90f * count);

            UpdateOffset();
            UpdateMask();
        }

        public void ReturnToStartPosition()
        {
            transform.position = _startPos;
        }

        public void SetStartScale()
        {
            transform.localScale = Vector3.one * _startScale;
        }

        public void Spawn(Vector3 position, Transform parent = null)
        {
            if (parent != null)
                transform.parent = parent;

            SetToPosition(position);
            _startPos = position;

            SetStartScale();
        }

        public void Despawn()
        {
            Destroy(this.gameObject);
        }

        #endregion

        #region Interface IDraggable

        public void OnDrop()
        {
            SetStartScale();
        }

        public void OnDragged()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            SetToPosition(mousePos);
        }

        public void OnStartDrag()
        {
            transform.localScale = Vector3.one;
        }

        #endregion

        #region Utils

        private void UpdateMask()
        {
            _mask.Clear();

            var tempScale = transform.localScale;

            transform.localScale = Vector3.one;

            foreach (Transform child in transform)
            {

                if (child.tag != BRICK_TAG) continue;

                var delta = child.position - transform.position;

                _mask.Add(new Vector2Int(Mathf.RoundToInt(delta.x), Mathf.RoundToInt(delta.y)));
            }

            transform.localScale = tempScale;

        }

        private void UpdateColor()
        {
            foreach (Transform child in transform)
            {

                if (child.tag != BRICK_TAG) continue;

                if (_spriteRenderer != null) break;

                _spriteRenderer = child.GetComponent<SpriteRenderer>();
            }

            _color = _spriteRenderer.color;
        }

        private void UpdateOffset()
        {
            _dragOffset = transform.position - _dragPivot.position;
        }

        protected override void Awake()
        {
            base.Awake();
            _mask = new List<Vector2Int>();

            UpdateOffset();
        }

        protected override void Start()
        {
            base.Start();

            UpdateMask();
            UpdateColor();
        }

        #endregion

    }
}
