using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Transform content;
    [SerializeField]
    private RectTransform ScreenPivot;
    [SerializeField]
    private OutfitContainer outfitContainerPrefab;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCamera;

    [Header("PageComponents")]
    [SerializeField]
    private InventoryPage clothesPage;
    [SerializeField]
    private InventoryPage hairsPage;
    [SerializeField]
    private InventoryPage accessoriesPage;

    [Header("Attributes")]
    [SerializeField]
    public InventoryType myType;

    private OutfitContainer currentOutfitContainer;
    private InventoryPage currentPage;
    private OutfitType currentScreen;
    private Stack<OutfitContainer> outfitStack = new Stack<OutfitContainer>();
    private List<OutfitContainer> currentOutfits = new List<OutfitContainer>();

    private void Start()
    {
        CreateContainers();
        InitializePages();
        SwitchScreen(clothesPage);
    }

    public void InitializePages()
    {
        clothesPage?.InitializePage(this, myType);
        hairsPage?.InitializePage(this, myType);
        accessoriesPage?.InitializePage(this, myType);
    }

    public void SwitchScreen(InventoryPage _newScreen)
    {
        currentPage?.ClosePage();
        _newScreen.OpenPage();
        currentPage = _newScreen;
    }

    public void SetPlayerOutfit(Outfit _outfitInfo)
    {
        player.SetClotheOutfit(_outfitInfo);
    }

    public void BuyOutfit(Outfit _outfitInfo)
    {
        player.BuyingOutfit(_outfitInfo);
    }

    public void AddNewInventoryOutfit(Outfit _outfitInfo)
    {
        switch (_outfitInfo.myType)
        {
            case OutfitType.Accessories:
                accessoriesPage.AddNewPageContainer(_outfitInfo);
                break;
            case OutfitType.Clothes:
                clothesPage.AddNewPageContainer(_outfitInfo);
                break;
            case OutfitType.Hairs:
                hairsPage.AddNewPageContainer(_outfitInfo);
                break;
        }
    }

    #region Click_Methods
    public void OnClick_HairPage()
    {
        SwitchScreen(hairsPage);
    }
    public void OnClick_ClothesPage()
    {
        SwitchScreen(clothesPage);
    }
    public void OnClick_AccessoriesPage()
    {
        SwitchScreen(accessoriesPage);
    }

    public void OnClick_OpenInventory()
    {
        if (player == null)
        {
            player = GameController.Instance.GetPlayer;
            virtualCamera.Follow = player.transform;
        }

        GameController.Instance.HideHUD();

        player.SwitchState(player.interactState);
        ScreenPivot.DOKill();
        ScreenPivot.DOAnchorPosY(0, 0.5f);
        accessoriesPage.CheckAllOutfits();
        clothesPage.CheckAllOutfits();
        hairsPage.CheckAllOutfits();
        if (virtualCamera != null)
            virtualCamera.enabled = true;

    }

    public void OnClick_CloseCloseInventory()
    {
        GameController.Instance.ShowHUD();
        player.SwitchState(player.idleState);
        ScreenPivot.DOKill();
        ScreenPivot.DOAnchorPosY(-600f, 0.5f);
        if(virtualCamera != null)
            virtualCamera.enabled = false;
    }
    #endregion

    #region Pooling
    private void CreateContainers()
    {
        for (int i = 0; i < 20; i++)
        {
            OutfitContainer temp = Instantiate(outfitContainerPrefab, content);
            temp.gameObject.SetActive(false);
            outfitStack.Push(temp);
        }
    }

    public OutfitContainer GetContainer()
    {
        if(outfitStack.Count > 0)
        {
            OutfitContainer temp = outfitStack.Pop();
            temp.gameObject.SetActive(true);
            return temp;
        }
        else
        {
            OutfitContainer temp = Instantiate(outfitContainerPrefab, content);
            temp.gameObject.SetActive(true);
            return temp;
        }
    }

    public void StoreContainer (OutfitContainer _container)
    {
        outfitStack.Push(_container);
    }
    #endregion
}