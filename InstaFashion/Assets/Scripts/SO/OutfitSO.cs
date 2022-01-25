using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItensSO/OutfitSO")]
public class OutfitSO : ScriptableObject
{
    public OutfitType type;
    public Outfit[] outfits;

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
}

[System.Serializable]
public class Outfit
{
    public string name;
    public int popularityStars = 1;
    public float price = 10;
    public bool unlocked;
    
    public InventoryType inventoryType= InventoryType.Store_1;
    public Color itemColor;
    public Sprite outlineIcon;
    public Sprite fillIcon;
    public RuntimeAnimatorController animator;

    public bool selected;
    [HideInInspector]
    public OutfitType myType;
}