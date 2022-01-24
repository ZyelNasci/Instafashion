using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClickBase : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler, IPointerClickHandler
{
    #region IPointer Methods
    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter(eventData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        PointerDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEndDrag(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PointerClick(eventData);
    }
    #endregion

    #region Virtual Methods
    protected virtual void PointerClick(PointerEventData eventData)
    {

    }
    protected virtual void PointerEnter(PointerEventData eventData)
    {

    }
    protected virtual void PointerExit(PointerEventData eventData)
    {

    }

    protected virtual void PointerDrag(PointerEventData eventData)
    {

    }

    protected virtual void PointerDown(PointerEventData eventData)
    {

    }
    protected virtual void PointerEndDrag(PointerEventData eventData)
    {

    }
    #endregion
}