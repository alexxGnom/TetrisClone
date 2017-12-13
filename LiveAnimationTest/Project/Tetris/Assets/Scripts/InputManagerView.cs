using UnityEngine;
using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;

namespace Tetris
{
    public class InputManagerView : View
    {
        private const string FIGURE_TAG = "Figure";
        private bool dragging = false;

        public IDraggableFigure Figure { get; private set; }

        public Signal StartDragSignal { get; private set; }

        public Signal DraggingSignal { get; private set; }

        public Signal DropSignal { get; private set; }

        public Signal EscapeSignal { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EscapeSignal.Dispatch();
                return;
            }

            if (Application.isMobilePlatform)
                MobileUpdate();
            else
                DesktopUpdate();
        }

        protected override void Awake()
        {
            base.Awake();

            StartDragSignal = new Signal();
            DraggingSignal = new Signal();
            DropSignal = new Signal();

            EscapeSignal = new Signal();
        }

        private void DesktopUpdate()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                StartDrag(pos);
            }

            if (Figure != null)
            {

                if (Input.GetMouseButton(0))
                {
                    Dragged();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    EndDrag();
                }
            }
        }

        private void MobileUpdate()
        {
            if (Input.touchCount != 1)
            {
                dragging = false;
                return;
            }

            Touch touch = Input.touches[0];
            Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                StartDrag(pos);
            }
            if (Figure != null)
            {
                if (dragging && touch.phase == TouchPhase.Moved)
                {
                    Dragged();
                }

                if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                {
                    EndDrag();
                }
            }
        }

        private void StartDrag(Vector3 pos)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(pos.x, pos.y), Vector2.zero, Mathf.Infinity);

            if (hit.collider != null)
            {
                Figure = hit.collider.GetComponent<FigureView>();
                if (Figure != null)
                {
                    dragging = true;
                    StartDragSignal.Dispatch();
                }
            }
        }

        private void Dragged()
        {
            DraggingSignal.Dispatch();
        }

        private void EndDrag()
        {
            DropSignal.Dispatch();
            Figure = null;
            dragging = false;
        }
    }
}
