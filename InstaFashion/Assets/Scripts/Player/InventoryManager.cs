using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Transform content;
    [SerializeField]
    private OutfitContainer outfitContainerPrefab;

    [SerializeField]
    private InventoryPage clothesPage;
    [SerializeField]
    private InventoryPage hairsPage;
    [SerializeField]
    private InventoryPage accessoriesPage;

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
        clothesPage?.InitializePage(this);
        hairsPage?.InitializePage(this);
        accessoriesPage?.InitializePage(this);
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