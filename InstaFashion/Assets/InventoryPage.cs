using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private Transform Content;

    [SerializeField]
    private OutfitSO outfitSO;

    private InventoryManager manager;
    private List<OutfitContainer> currentOutfits = new List<OutfitContainer>();

    private OutfitType myType;

    public void InitializePage(InventoryManager _manager)
    {
        manager = _manager;
        SetContainer();
    }

    public void OpenPage()
    {
        gameObject.SetActive(true);
    }

    public void ClosePage()
    {
        gameObject.SetActive(false);
    }

    private void SetContainer()
    {
        currentOutfits.Clear();
        Outfit[] outfits = outfitSO.outfits;
        for (int i = 0; i < outfits.Length; i++)
        {
            OutfitContainer temp = manager.GetContainer();
            outfits[i].myType = outfitSO.type;
            temp.transform.SetParent(Content);
            temp.SetContainer(manager, outfits[i], outfits[i].itemColor);
            currentOutfits.Add(temp);
        }
    }
}