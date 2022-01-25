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
    private CameraDrag dragCamera;
    [SerializeField]
    private Camera photoCam;
    [SerializeField]
    private SmartphoneManager manager;
    [SerializeField]
    private GameObject camGroup;
    [SerializeField]
    private GameObject confirmationGroup;
    [SerializeField]
    private RenderTexture renderTexture;

    private bool Screenshot;
    public int photoIndex { get; private set; }

    private Texture2D currenTexture;
    private int width = 248;
    private int height = 248;

    private bool tutorial = true;

    public void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderCamera;
        dragCamera.ResetCameraPosition();
    }

    public void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderCamera;
        photoCam.targetTexture = renderTexture;
        confirmationGroup.SetActive(false);
        camGroup.SetActive(true);
    }

    public void OnSlider_Zoom(float _value)
    {
        Debug.Log("Value: " + _value);
        photoCam.orthographicSize = _value;
    }
    public void OnClick_Back()
    {
        if (!tutorial)
            manager.SwitchScreen(SmartphoneScreen.Home);
    }

    private void RenderCamera(ScriptableRenderContext arg1, Camera arg2)
    {
        if (Screenshot)
        {
            currenTexture = null;
            Screenshot = false;

            currenTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, width, height);
            currenTexture.ReadPixels(rect, 0, 0);
            currenTexture.Apply();
            OpenConfirmationScreen();
        }
    }
    public void OpenConfirmationScreen()
    {
        confirmationGroup.SetActive(true);
        camGroup.SetActive(false);
    }

    public void OnClick_PostPhoto()
    {
        photoCam.targetTexture = renderTexture;

        confirmationGroup.SetActive(false);
        camGroup.SetActive(true);

        Sprite _sprite = Sprite.Create(currenTexture, new Rect(0, 0, width, height), new Vector2(width * 0.5f, width * 0.5f));

        if (!tutorial)
        {
            manager.AddPhotoOnGallery(_sprite);            
        }
        else
        {
            manager.AddPerfilPhoto(_sprite);
            tutorial = false;
        }        
    }
    public void OnClick_Delete()
    {
        photoCam.targetTexture = renderTexture;
        dragCamera.ResetCameraPosition();
        confirmationGroup.SetActive(false);
        camGroup.SetActive(true);
    }

    public void TakeScreenshot()
    {
        photoCam.targetTexture = RenderTexture.GetTemporary(248,248, 16);
        Screenshot = true;
    }
}