using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItensSO/OutfitSO")]
[System.Serializable]
public class OutfitSO : ScriptableObject
{
    public OutfitType type;
    public Outfit[] outfits;

    public void RestoreOutfitsValue(Outfit[] _outfits)
    {
        for (int i = 0; i < outfits.Length; i++)
        {
            outfits[i].RestoreValue(_outfits[i]);
        }
    }

    public List<Outfit> GetOnlyInventoryType(InventoryType _type)
    {
        List<Outfit> array = new List<Outfit>();
        for (int i = 0; i < outfits.Length; i++)
        {
            if (outfits[i].inventoryType == _type)
            {
                array.Add(outfits[i]);
            }
        }
        return array;
    }

    public void ResetSO()
    {
        for (int i = 0; i < outfits.Length; i++)
        {
            if(i > 0)            
                outfits[i].ResetIndividualOutfit(false);            
            else            
                outfits[i].ResetIndividualOutfit(true);            
        }
    }

    public bool Reset;
    private void OnValidate()
    {
        if (Reset)
        {
            ResetSO();
            Reset = false;
        }
    }
}

[System.Serializable]
public class Outfit
{
    public string name;
    public int popularityStars = 1;
    
    public float price = 10;
    public bool unlocked;
    
    public InventoryType inventoryType = InventoryType.Store_1;
    public Color itemColor;
    public Sprite outlineIcon;
    public Sprite fillIcon;
    public RuntimeAnimatorController animator;

    public int currentPopularityStar;    
    public bool selected;
    
    [HideInInspector]
    public OutfitType myType;

    public void ResetIndividualOutfit(bool _value)
    {
        unlocked = _value;
        selected = _value;
        currentPopularityStar = popularityStars;
    }

    public void RestoreValue(Outfit _outfit)
    {
        unlocked = _outfit.unlocked;
        currentPopularityStar = _outfit.currentPopularityStar;
        selected = _outfit.selected;
        itemColor = _outfit.itemColor;
}
}