using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameData")]
public class GameData : ScriptableObject
{
    [Header("Player Data")]
    public int Money;
    public string playerName;
    public Color colorSkin;

    [Header("Smartphone Data")]
    public int totalLikes;
    public int totalFollowers;
    public bool tutorial;

    [Header("Inventory Data")]
    public OutfitSO[] outfitsSO;

    public void Setvalues(DataInfo _data)
    {
        Money = _data.Money;
        playerName = _data.playerName;
        colorSkin = _data.colorSkin;
        totalLikes = _data.totalLikes;
        totalFollowers = _data.totalFollowers;
        tutorial = _data.tutorial;
    }

}