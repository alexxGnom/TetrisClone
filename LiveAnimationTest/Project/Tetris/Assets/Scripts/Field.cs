using System.Collections;
using UnityEngine;

namespace Tetris
{
    public interface IField
    {
        int Width { get; }
        int Heigh { get; }

        bool IsCellBusy(Vector2Int cellPos);

        bool CanSetFigureToField(IFigure figure, Vector2Int fieldAdress);

        bool SetFigure(IFigure figure);

        void ClearField();
    }

    public class Field : MonoBehaviour, IField
    {
        #region Unity properties

        [SerializeField]
        private int _width = 10;

        [SerializeField]
        private int _heigh = 10;

        [SerializeField]
        private GameObject _blockPrefab;

        #endregion

        #region Private fields

        private IFieldBlock[,] _field;

        #endregion

        #region Injects

        [Inject]
        public RemoveLineSignal RemoveLineSignal { get; private set; }

        #endregion

        #region Interface

        public int Width { get { return _width; } }
        public int Heigh { get { return _heigh; } }

        public bool IsCellBusy(Vector2Int cellPos)
        {
            return _field[cellPos.x, cellPos.y].IsActive;
        }

        public bool CanSetFigureToField(IFigure figure, Vector2Int fieldAdress)
        {
            foreach (var delta in figure.Mask)
            {
                if (!CanDrop(fieldAdress + delta)) return false;
            }

            return true;
        }

        public bool SetFigure(IFigure figure)
        {
            var figureOnFieldPosition = transform.worldToLocalMatrix.MultiplyPoint(figure.Position);

            var fieldAdress = new Vector2Int(Mathf.RoundToInt(figureOnFieldPosition.x), Mathf.RoundToInt(figureOnFieldPosition.y));

            if (!CanSetFigureToField(figure, fieldAdress)) return false;

            foreach (var delta in figure.Mask)
                ActivateBlock(fieldAdress + delta, figure.BrickColor);

            StartCoroutine(RemoveLinesRoutine());
            return true;
        }

        public void ClearField()
        {
            foreach (var b in _field)
                b.Deactivate();
        }

        #endregion

        #region Utils

        private void Awake()
        {
            _field = new Block[_width, _heigh];

            CreateField();
        }

        private bool CanDrop(Vector2Int cellPos)
        {
            return cellPos.x >= 0
                    && cellPos.x < _width
                    && cellPos.y >= 0
                    && cellPos.y < _heigh
                    && !IsCellBusy(cellPos);
        }

        private void CreateField()
        {
            for (var y = 0; y < _heigh; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    var block = Instantiate(_blockPrefab, transform).GetComponent<Block>();
                    block.transform.localPosition = new Vector3(x, y, 0);
                    block.Adress = new Vector2Int(x, y);
                    block.name = string.Format("Block {0} {1}", x, y);
                    block.Deactivate();

                    _field[x, y] = block;
                }
            }
        }

        private void ActivateBlock(Vector2Int blockAdress, Color color)
        {
            var block = _field[blockAdress.x, blockAdress.y];
            block.Activate();
            block.SetColor(color);
        }

        private bool IsRowFull(int y)
        {
            for (var x = 0; x < _width; x++)
            {
                if (!_field[x, y].IsActive)
                    return false;
            }

            return true;
        }

        private bool IsColumnFull(int x)
        {
            for (var y = 0; y < _heigh; y++)
            {
                if (!_field[x, y].IsActive)
                    return false;
            }

            return true;
        }

        private void RemoveRow(int y)
        {
            for (var x = 0; x < _width; x++)
                _field[x, y].Deactivate();

            RemoveLineSignal.Dispatch(_width);
        }

        private void RemoveCol(int x)
        {
            for (var y = 0; y < _heigh; y++)
                _field[x, y].Deactivate();

            RemoveLineSignal.Dispatch(_heigh);
        }

        private void RemoveLines()
        {
            for (var y = 0; y < _heigh; y++)
            {
                if (IsRowFull(y))
                {
                    RemoveRow(y);
                }
            }

            for (var x = 0; x < _width; x++)
            {
                if (IsColumnFull(x))
                {
                    RemoveCol(x);
                }
            }
        }

        private IEnumerator RemoveLinesRoutine()
        {
            yield return new WaitForEndOfFrame();
            RemoveLines();
        }

        #endregion

    }
}

