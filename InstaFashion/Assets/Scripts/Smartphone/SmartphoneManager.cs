using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class SmartphoneManager : MonoBehaviour
{
    #region Variables
    [Header("Perfil Components")]
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
    private Image photoPerfil;
    [SerializeField]
    private Image whiteFade;
    [Header("Groups")]
    [SerializeField]
    private RectTransform SmartGroup;
    [SerializeField]
    private GameObject miniButton;
    [SerializeField]
    private GameObject homeGroup;
    [SerializeField]
    private GameObject cameraGroup;
    [SerializeField]
    private SmartphoneCreateCharacter createCharacterGroup;
    public SmartphoneScreen currentScreen { get; private set; }

    private int totalLikes;
    private int totalFolowers = 0;

    private Coroutine likeCoroutine;
    private Coroutine followCoroutine;

    [SerializeField]
    private Transform content;

    private Stack<PhotoContainer> containerStack = new Stack<PhotoContainer>();
    private int currentLikes;
    private SaveSystem save { get { return SaveSystem.Instance; }}
    #endregion

    public void Start()
    {
        CreateContainers();
    }

    #region other methods
    public void LoadSmartphone(GameData _data)
    {
        SetPerfilname(_data.playerName);
        totalLikes = _data.totalLikes;
        totalFolowers = _data.totalFollowers;

        likeText.text = totalLikes.ToString("00");
        followersText.text = totalFolowers.ToString("00");

        for (int i = 0; i < _data.sprites.Count; i++)
        {
            if (i > 0)
            {
                PhotoContainer temp = GetContainer();
                temp.transform.SetAsFirstSibling();
                temp.SetPhotoContainer(_data.sprites[i], 0);
            }
            else
            {
                photoPerfil.sprite = _data.sprites[i];
            }
        }
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
                createCharacterGroup.gameObject.SetActive(false);
                whiteFade.DOFade(0, 1f);
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
                createCharacterGroup.gameObject.SetActive(true);
                createCharacterGroup.InitializeScreen();
                whiteFade.DOFade(1, 0);
                break;
        }
        currentScreen = _newScreen;
    }
    #endregion

    #region Like/Followers Methods
    IEnumerator DelayToLike()
    {
        float initiTime = Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(initiTime);

        while (currentLikes > 0)
        {
            int likeValue = Random.Range(1, currentLikes + 1);
            float timer = Random.Range(0.2f, 2f);
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
            float timer = Random.Range(0.2f, 3f);
            yield return new WaitForSeconds(timer);
            AddFollower(likeValue);
            currentFollowers -= likeValue;
        }
        followCoroutine = null;
    }

    public void AddLikes(int _value)
    {
        totalLikes += _value;
        save.dataSO.totalLikes = totalLikes;
        likeText.text = totalLikes.ToString("00");
        likeGroup.DOKill();
        likeGroup.localScale = Vector3.one;
        likeGroup.DOScale(Vector3.one * 1.1f, 0.2f).SetLoops(2, LoopType.Yoyo);

        int money = Random.Range(1, 5) * _value;
        GameController.Instance.UpdateMoney(money);    
    }

    public void AddFollower(int _value)
    {
        totalFolowers += _value;
        save.dataSO.totalFollowers= totalFolowers;
        followersText.text = totalFolowers.ToString("00");
        followerGroup.DOKill();
        followerGroup.localScale = Vector3.one;
        followerGroup.DOScale(Vector3.one * 1.1f, 0.2f).SetLoops(1, LoopType.Yoyo);
    }
    #endregion

    #region Perfil Methods
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
        save.dataSO.playerName = _name;
    }
    #endregion

    #region OnClick Methods
    public void OnClick_OpenSmartphone()
    {
        SwitchScreen(SmartphoneScreen.Home);
        miniButton.SetActive(false);
        player.SwitchState(player.interactState);
        SmartGroup.DOKill();
        SmartGroup.DOAnchorPosY(0, 0.5f);
    }
    public void Open_CreateCharacter()
    {
        SwitchScreen(SmartphoneScreen.CreateCharacter);
        miniButton.SetActive(false);
        player.SwitchState(player.interactState);
        SmartGroup.DOKill();
        SmartGroup.DOAnchorPosY(0, 0.5f);
    }
    public void OnClick_CloseSmartphone()
    {
        if (currentScreen == SmartphoneScreen.CreateCharacter) return;
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
