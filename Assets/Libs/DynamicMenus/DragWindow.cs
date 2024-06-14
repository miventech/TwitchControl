using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


namespace Multplex.SXP.Utils
{
    public class DragWindow : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerExitHandler, IEndDragHandler
    {
        public RectTransform ParentWindowMain;
        public RectTransform WindowMain;
        private Vector2 _pointerOffset;
        private Vector2 _localPointerPos;

        public UnityEvent OnEndDrag;
        // public bool ExecutedInit = true;
        void Start()
        {
            Init();
        }

        // public jNodesWindow getJNW()
        // {
        //     return jnsw;
        // }
        public void Init()
        {
            if (WindowMain == null)
            {
                WindowMain = GetComponent<RectTransform>();
            }
            Vector3 postion = WindowMain.position;
            WindowMain.anchorMin = Vector2.zero;
            WindowMain.anchorMax = Vector2.zero;
            WindowMain.position = postion;

            if (ParentWindowMain == null)
            {
                findWindowNode();
                // GetComponent<jNode>().readyJDrag();
            }
        }

        void findWindowNode()
        {
            ParentWindowMain = WindowMain.parent.GetComponent<RectTransform>();
            //commentar 



            // jNodesWindow jnw = null;
            // Transform __parent = transform;
            // for (int y = 0; y < 6; y++)
            // {
            //     if (__parent.parent == null) break;
            //     __parent = __parent.parent;

            //     if (__parent.GetComponent<jNodesWindow>() != null)
            //     {
            //         jnw = __parent.GetComponent<jNodesWindow>();
            //         break;
            //     }
            // }
            // if (jnw == null)
            // {
            //     Destroy(gameObject);
            // }
            // else
            // {
            //     jnsw = jnw.GetComponent<jNodesWindow>();
            //     ParentWindowMain = jnw.GetComponent<RectTransform>();
            // }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            WindowMain.SetAsLastSibling();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(WindowMain, eventData.position, eventData.pressEventCamera, out _pointerOffset);
            // if (jnsw != null)
            // {
            //     jnsw.drawing = true;
            // }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // if (jnsw != null)
            // {
            //     jnsw.drawing = false;
            // }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // if (jnsw != null)
            // {
            //     jnsw.drawing = false;
            // }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Vector2 pointerPos = ClampToWindow(eventData);
                var success = RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentWindowMain, pointerPos, eventData.pressEventCamera, out _localPointerPos);
                if (success)
                {
                    WindowMain.localPosition = _localPointerPos - _pointerOffset;
                }
            }

            // if (jnsw != null)
            // {
            //     jnsw.drawing = true;
            // }
        }

        private Vector2 ClampToWindow(PointerEventData eventData)
        {
            var rawPointerPos = eventData.position;
            var canvasCorners = new Vector3[4];
            ParentWindowMain.GetWorldCorners(canvasCorners);

            var clampedX = Mathf.Clamp(rawPointerPos.x, canvasCorners[0].x, canvasCorners[2].x);
            var clampedY = Mathf.Clamp(rawPointerPos.y, canvasCorners[0].y, canvasCorners[2].y);

            var newPointerPos = new Vector2(clampedX, clampedY);
            return newPointerPos;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            OnEndDrag?.Invoke();
        }
    }

}