using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Pangoo.Core.VisualScripting
{

    public class UIClueItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Camera MainCamera;
        public float Distance = 0.2f;
        public RectTransform rectTransform;

        public RectTransform CanvasRect;                //控件所在画布
        public Vector3 pos;                            //控件初始位置
        public Vector2 mousePos;                       //鼠标初始位置


        public RectTransform ClueListRect;

        public Canvas Canvas;

        public RectTransform TargetRect;

        public bool IsPointEnter;

        public bool IsInCluesList
        {
            get
            {
                return transform.IsChildOf(ClueListRect);
            }
        }

        public bool IsInTargetList
        {
            get
            {
                return transform.IsChildOf(TargetRect);
            }
        }

        public TextMeshProUGUI Text;



        private void Start()
        {

        }



        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("开始拖拽");
            //控件所在画布空间的初始位置
            transform.SetParent(CanvasRect);
            pos = rectTransform.anchoredPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, eventData.position, eventData.pressEventCamera, out mousePos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 newVec;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, eventData.position, eventData.pressEventCamera, out newVec);
            //鼠标移动在画布空间的位置增量
            Vector3 offset = new Vector3(newVec.x - mousePos.x, newVec.y - mousePos.y, 0);
            //原始位置增加位置增量即为现在位置
            Debug.Log($"newVec:{newVec},  .anchoredPosition:{(this.transform as RectTransform).anchoredPosition} offset:{offset}");
            // (this.transform as RectTransform).anchoredPosition = newVec;
            (this.transform as RectTransform).anchoredPosition = pos + offset;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (TargetRect != null)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(TargetRect, Input.mousePosition))
                {
                    transform.SetParent(TargetRect);
                    return;
                }
            }

            transform.SetParent(ClueListRect);
            Debug.Log("结束拖拽");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsPointEnter = true;
            Text.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsPointEnter = false;
            Text.gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsInCluesList)
            {
                transform.SetParent(TargetRect);
                return;
            }

            if (IsInTargetList)
            {
                transform.SetParent(ClueListRect);
                return;
            }
        }
    }
}