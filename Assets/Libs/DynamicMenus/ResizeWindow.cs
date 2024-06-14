
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace Multplex.SXP.Utils
{
    public class ResizeWindow : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,IPointerUpHandler,IEndDragHandler
    {
        public Vector2 minSize;
        public Vector2 maxSize;
        private Vector2 currentPointerPosition;
        private Vector2 previousPointerPosition;
        public Vector2 MultAxis = new Vector2(1,1);
        public RectTransform rectTransform;
        public Texture2D IconSpanWindow;
        readonly Vector2 HostPotCursor = new Vector2(15,12);
        bool transforming = false;
        public UnityEvent ChangeZise;
        // void Awake()
        // {
        //     rectTransform = transform.parent.GetComponent<RectTransform>();
        // }

        
        public void OnPointerDown(PointerEventData data)
        {
            rectTransform.SetAsLastSibling();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out previousPointerPosition);
            transforming = true;
            Cursor.SetCursor(IconSpanWindow, HostPotCursor, CursorMode.Auto);
            
        }

        public void OnDrag(PointerEventData data)
        {
            if (rectTransform == null)
                return;

            Vector2 sizeDelta = rectTransform.sizeDelta;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out currentPointerPosition);
            Vector2 resizeValue = currentPointerPosition - previousPointerPosition;
            Debug.Log(resizeValue);
            sizeDelta += new Vector2(resizeValue.x * MultAxis.x, -resizeValue.y * MultAxis.y);
            sizeDelta = new Vector2(
                Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
                Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
                );
            Debug.Log(sizeDelta);
            rectTransform.sizeDelta = sizeDelta;

            previousPointerPosition = currentPointerPosition;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Cursor.SetCursor(IconSpanWindow, HostPotCursor, CursorMode.Auto);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (transforming) return;
            Cursor.SetCursor(null, HostPotCursor, CursorMode.Auto);
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transforming = false;
            Cursor.SetCursor(null, HostPotCursor, CursorMode.Auto);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ChangeZise.Invoke();
        }
    }
}