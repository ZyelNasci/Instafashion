using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class PhotoCam : PointerClickBase
{
    [SerializeField]
    private Camera photoCam;
    [SerializeField]
    private RectTransform rect;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private SmartphoneManager manager;
    [SerializeField]
    private Image screen;

    private Vector3 mOffset;
    private float mZCoord;
    private Mouse myMouse;
    private Camera myCam;

    private bool Screenshot;

    public int photoIndex { get; private set; }

    public void Awake()
    {
        myCam = Camera.main;
        myMouse = Mouse.current;  
    }

    public void OnEnable()
    {
        Vector3 newPos = myCam.ScreenToWorldPoint(rect.position);
        newPos.z = photoCam.transform.position.z;
        newPos.y += 1f;
        photoCam.transform.position = newPos;

        RenderPipelineManager.endCameraRendering += DoTest;
    }

    public void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= DoTest;
    }


    protected override void PointerDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;

        Vector3 newPos = myCam.ScreenToWorldPoint(rect.position);
        newPos.z = photoCam.transform.position.z;
        newPos.y += 1f;
        photoCam.transform.position = newPos;
    }

    public void OnSlider_Zoom(float _value)
    {
        Debug.Log("Value: " + _value);
        photoCam.orthographicSize = _value;
    }
    public void OnClick_Back()
    {
        manager.SwitchScreen(SmartphoneScreen.Home);
    }


    private void DoTest(ScriptableRenderContext arg1, Camera arg2)
    {
        if (Screenshot)
        {
            Screenshot = false;

            int width = 248;
            int height = 248;

            Texture2D renderResult = new Texture2D(width, height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, width, height);
            renderResult.ReadPixels(rect, 0, 0);
            renderResult.Apply();

            Sprite _sprite = Sprite.Create(renderResult, rect, new Vector2(width * 0.5f, width * 0.5f));

            byte[] byteArray = renderResult.EncodeToPNG();
            string path = Application.dataPath + "/Screenshots/CameraScreenshot_" + photoIndex.ToString("00") + ".png";
            photoIndex++;
            Debug.Log("Path: " + path);
            System.IO.File.WriteAllBytes(path, byteArray);

            manager.AddPhotoOnGallery(_sprite);
        }
    }
    public void TakeScreenshot()
    {
        //photoCam.targetTexture = RenderTexture.GetTemporary((int)248.25f, (int)248.25, 16);
        Screenshot = true;
    }
}