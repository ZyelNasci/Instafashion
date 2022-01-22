using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItensSO/OutfitSO")]
public class OutfitSO : ScriptableObject
{
    public Outfit[] outfits;
}

[System.Serializable]
public class Outfit
{
    public string name;
    public int popularityStars = 1;
    public Color itemColor;
    public Sprite outlineIcon;
    public Sprite fillIcon;
    public RuntimeAnimatorController animator;
}