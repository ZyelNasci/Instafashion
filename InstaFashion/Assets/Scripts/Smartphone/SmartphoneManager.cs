using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

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
    private Transform likeGroup;
    [SerializeField]
    private Transform followerGroup;
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
    private GameObject createCharacterGroup;
    [SerializeField]
    private Image photoPerfil;
    [SerializeField]
    private SmartphoneScreen currentScreen;

    private int totalLikes;
    private int totalFolowers = 0;

    private Coroutine likeCoroutine;
    private Coroutine followCoroutine;

    [SerializeField]
    private Transform content;

    private Stack<PhotoContainer> containerStack = new Stack<PhotoContainer>();

    public void Start()
    {
        CreateContainers();
        SwitchScreen(currentScreen);

        GameData dataSO = Resources.Load<GameData>("Scriptables/GameData");
        SaveSystem.Instance.LoadGame();
        photoPerfil.sprite = dataSO.GetPhoto();
    }

    private int currentLikes;
    IEnumerator DelayToLike()
    {
        float initiTime = Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(initiTime);

        while (currentLikes > 0)
        {
            int likeValue = Random.Range(1, currentLikes + 1);
            float timer = Random.Range(0.2f, 1.5f);
            yield return new WaitForSeconds(timer);
            AddLikes(likeValue);
            currentLikes -= likeValue;
        }

        likeCoroutine = null;
    }
    private int currentFollowers;
    IEnumerator DelayToFollowers()
    {
        float initiTime = Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(initiTime);
        while (currentFollowers > 0)
        {
            int likeValue = Random.Range(1, currentFollowers + 1);
            float timer = Random.Range(0.2f, 1.5f);
            yield return new WaitForSeconds(timer);
            AddFollower(likeValue);
            currentFollowers -= likeValue;
        }
        followCoroutine = null;
    }


    public void AddLikes(int _value)
    {
        totalLikes += _value;
        likeText.text = totalLikes.ToString("00");
        likeGroup.DOKill();
        likeGroup.localScale = Vector3.one;
        likeGroup.DOScale(Vector3.one * 1.1f, 0.2f).SetLoops(2, LoopType.Yoyo);
    }
    public void AddFollower(int _value)
    {
        totalFolowers += _value;
        followersText.text = totalFolowers.ToString("00");
        followerGroup.DOKill();
        followerGroup.localScale = Vector3.one;
        followerGroup.DOScale(Vector3.one * 1.1f, 0.2f).SetLoops(1, LoopType.Yoyo);
    }

    public void AddPhotoOnGallery(Sprite _sprite)
    {
        float percentage    = player.GetTotalPopularityOutift() * 0.03f;
        int likes           = Mathf.CeilToInt(percentage * totalFolowers);

        float followerPercentage = Random.Range(0.3f, 0.5f);
        currentFollowers    += Mathf.CeilToInt(likes * followerPercentage);
        currentLikes        += likes;

        if (followCoroutine == null)
            followCoroutine = StartCoroutine(DelayToFollowers());
        if (likeCoroutine == null)
            likeCoroutine = StartCoroutine(DelayToLike());
        

        PhotoContainer temp = GetContainer();
        temp.transform.SetAsFirstSibling();
        temp.SetPhotoContainer(_sprite, likes);
        SwitchScreen(SmartphoneScreen.Home);
    }

    public void AddPerfilPhoto(Sprite _sprite)
    {
        photoPerfil.sprite = _sprite;
        SwitchScreen(SmartphoneScreen.Home);
        AddFollower(10);
    }
    public void SetPerfilname(string _name)
    {
        nameText.text = "@" + _name;
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
            case SmartphoneScreen.CreateCharacter:
                createCharacterGroup.SetActive(false);
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
            case SmartphoneScreen.CreateCharacter:
                createCharacterGroup.SetActive(true);
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
            temp.InitializeContainer(this);
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
