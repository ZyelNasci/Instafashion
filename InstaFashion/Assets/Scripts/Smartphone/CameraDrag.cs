using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class CameraDrag : PointerClickBase
{
    [SerializeField]
    private RectTransform rect;
    [SerializeField]
    private Camera photoCam;
    [SerializeField]
    private Canvas canvas;
    
    private Camera mainCamera;

    public void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnEnable()
    {
        //ResetCameraPosition();
    }

    public void ResetCameraPosition()
    {
        Vector3 newPos = mainCamera.ScreenToWorldPoint(rect.position);
        newPos.z = photoCam.transform.position.z;
        newPos.y += 1f;
        photoCam.transform.position = newPos;
    }

    protected override void PointerDrag(PointerEventData eventData)
    {
        Vector2 newRectPos = rect.anchoredPosition;
        newRectPos += eventData.delta / canvas.scaleFactor;

        newRectPos.x = Mathf.Clamp(newRectPos.x, -344, 355f);
        newRectPos.y = Mathf.Clamp(newRectPos.y, -216, 62);

        rect.anchoredPosition = newRectPos;


        

        Vector3 newPos = mainCamera.ScreenToWorldPoint(rect.position);
        newPos.z = photoCam.transform.position.z;
        newPos.y += 1f;
        photoCam.transform.position = newPos;
    }
}
