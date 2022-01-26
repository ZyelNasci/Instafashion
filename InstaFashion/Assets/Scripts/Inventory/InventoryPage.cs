using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    protected Transform Content;
    [SerializeField]
    protected OutfitSO outfitSO;

    protected InventoryManager manager;
    public List<OutfitContainer> currentOutfits = new List<OutfitContainer>();

    public void InitializePage(InventoryManager _manager, InventoryType _type)
    {
        manager = _manager;
        SetContainer(_type);
        //SetContainer(manager.myType);
    }

    public void OpenPage()
    {
        gameObject.SetActive(true);
    }

    public void ClosePage()
    {
        gameObject.SetActive(false);
    }

    public void CheckAllOutfits()
    {
        for (int i = 0; i < currentOutfits.Count; i++)
        {
            currentOutfits[i].CheckPopularity();
        }
    }

    public virtual void SetContainer(InventoryType type)
    {
        currentOutfits.Clear();
        Outfit[] outfits = outfitSO.outfits;
        Debug.Log(type);
        for (int i = 0; i < outfits.Length; i++)
        {
            if (type == outfits[i].inventoryType|| 
                type == InventoryType.PlayerInventory && outfits[i].unlocked)
            {
                OutfitContainer temp    = manager.GetContainer();
                outfits[i].myType       = outfitSO.type;
                temp.transform.parent   = Content;  
                temp.transform.SetAsLastSibling();
                temp.SetContainer(manager, outfits[i]);
                currentOutfits.Add(temp);                
            }
        }
    }

    public void AddNewPageContainer(Outfit _outfitInfo)
    {
        OutfitContainer temp = manager.GetContainer();
        _outfitInfo.myType = outfitSO.type;
        temp.transform.parent = Content;
        temp.transform.SetAsLastSibling();
        temp.SetContainer(manager, _outfitInfo);
        currentOutfits.Add(temp);
    }
}