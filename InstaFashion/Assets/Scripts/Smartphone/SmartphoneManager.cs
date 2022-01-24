using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class SmartphoneManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI likeText;
    [SerializeField]
    private TextMeshProUGUI followersText;
    [SerializeField]
    private PhotoContainer prefab;
    [SerializeField]
    private RectTransform SmartGroup;
    [SerializeField]
    private GameObject miniButton;
    [SerializeField]
    private GameObject homeGroup;
    [SerializeField]
    private GameObject cameraGroup;
    [SerializeField]
    private SmartphoneScreen currentScreen;

    private int totalLikes;
    private int totalFolowers;

    [SerializeField]
    private Transform content;

    private Stack<PhotoContainer> containerStack = new Stack<PhotoContainer>();

    public void Start()
    {
        CreateContainers();
        SwitchScreen(SmartphoneScreen.None);
    }

    public void AddPhotoOnGallery(Sprite _sprite)
    {
        PhotoContainer temp = GetContainer();
        temp.transform.SetAsFirstSibling();
        temp.SetPhotoContainer(_sprite);
        SwitchScreen(SmartphoneScreen.Home);
    }

    public void SwitchScreen(SmartphoneScreen _newScreen)
    {
        switch (currentScreen)
        {
            case SmartphoneScreen.None:
                break;
            case SmartphoneScreen.Home:
                homeGroup.SetActive(false);
                break;
            case SmartphoneScreen.Camera:
                cameraGroup.SetActive(false);
                break;
        }
        switch (_newScreen)
        {
            case SmartphoneScreen.None:
                break;
            case SmartphoneScreen.Home:
                homeGroup.SetActive(true);
                break;
            case SmartphoneScreen.Camera:
                cameraGroup.SetActive(true);
                break;
        }
        currentScreen = _newScreen;
    }

    #region OnClick Methods
    public void OnClick_OpenSmartphone()
    {
        SwitchScreen(SmartphoneScreen.Home);
        miniButton.SetActive(false);
        player.SwitchState(player.interactState);
        SmartGroup.DOKill();
        SmartGroup.DOAnchorPosY(0, 0.5f);
    }
    public void OnClick_CloseSmartphone()
    {
        SwitchScreen(SmartphoneScreen.None);
        player.SwitchState(player.idleState);
        SmartGroup.DOKill();
        SmartGroup.DOAnchorPosY(-600, 0.5f).OnComplete(() => miniButton.SetActive(true));
    }
    public void OnClick_OpenCameraGroup()
    {
        SwitchScreen(SmartphoneScreen.Camera);
    }
    #endregion

    #region Pooling
    private void CreateContainers()
    {
        for (int i = 0; i < 12; i++)
        {
            PhotoContainer temp = Instantiate(prefab, content);
            containerStack.Push(temp);
        }
    }

    public PhotoContainer GetContainer()
    {
        if (containerStack.Count > 0)
        {
            PhotoContainer temp = containerStack.Pop();
            return temp;
        }
        else
        {
            PhotoContainer temp = Instantiate(prefab, content);
            return temp;
        }
    }

    public void StoreContainer(PhotoContainer _container)
    {
        containerStack.Push(_container);
    }
    #endregion
}
