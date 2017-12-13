using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public interface IFigureGenerator
    {
        void Generate();

        List<IFigure> GetCurrentFigures();

        void UseFigure(IFigure figure);

        void ClearFigures();

    }

    public class FigureGenerator : MonoBehaviour, IFigureGenerator
    {
        #region Unity properties

        [SerializeField]
        private Transform[] _slotAnchors;

        [SerializeField]
        private GameObject[] _prefabs;

        #endregion

        #region Injects

        [Inject]
        public FindMovesSignal FindMovesSignal { get; private set; }

        #endregion

        #region Private fields

        private List<IFigure> _figures;

        #endregion

        #region Interface

        public void Generate()
        {
            for (var i = 0; i < _slotAnchors.Length; i++)
            {
                var figure = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)]).GetComponent<FigureView>();

                figure.Rotate(Random.Range(0, 4));
                figure.Spawn(_slotAnchors[i].position, _slotAnchors[i]);
                _figures.Add(figure);
            }
        }

        public List<IFigure> GetCurrentFigures()
        {
            return _figures;
        }

        public void UseFigure(IFigure figure)
        {
            _figures.Remove(figure);
            CheckForGenerate();
        }

        public void ClearFigures()
        {
            if (_figures.Count == 0) return;

            foreach (var f in _figures)
            {
                f.Despawn();
            }
            _figures.Clear();
        }

        #endregion

        #region Utils

        private void Awake()
        {
            _figures = new List<IFigure>();
        }

        private void CheckForGenerate()
        {
            if (_figures.Count <= 0)
                Generate();

            FindMovesSignal.Dispatch();

        }

        #endregion
    }
}
