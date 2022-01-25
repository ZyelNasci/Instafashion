using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataInfo
{    
    public int Money;
    public string playerName;
    public Color colorSkin;
    
    public int totalLikes;
    public int totalFollowers;
    public bool tutorial;

    public DataInfo(GameData _data)
    {
        Money = _data.Money;
        playerName = _data.playerName;
        colorSkin = _data.colorSkin;
        totalLikes = _data.totalLikes;
        totalFollowers = _data.totalFollowers;
        tutorial = _data.tutorial;        
    }

    public void RestoreValues(GameData _data)
    {
        _data.Money = Money;
        _data.playerName = playerName;
        _data.colorSkin = colorSkin;
        _data.totalLikes = totalLikes;
        _data.totalFollowers = totalFollowers;
        _data.tutorial = tutorial;
    }
}
