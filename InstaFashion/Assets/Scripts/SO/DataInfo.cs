using System;
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

    public Outfit[] outfits;
    public string[] photos;

    public DataInfo(GameData _data)
    {
        Money           = _data.Money;
        playerName      = _data.playerName;
        colorSkin       = _data.colorSkin;
        totalLikes      = _data.totalLikes;
        totalFollowers  = _data.totalFollowers;
        tutorial        = _data.tutorial;

        photos = new string[_data.photosBytes.Count];

        for (int i = 0; i < photos.Length; i++)
        {
            if(photos[i] != _data.photosBytes[i])
                photos[i] = _data.photosBytes[i];
        }

        for (int i = 0; i < _data.outfitsSO.Length; i++)
        {
            outfits = _data.outfitsSO[i].outfits;
        }
    }

    public void RestoreValues(GameData _data)
    {
        _data.Money             = Money;
        _data.playerName        = playerName;
        _data.colorSkin         = colorSkin;
        _data.totalLikes        = totalLikes;
        _data.totalFollowers    = totalFollowers;
        _data.tutorial          = tutorial;


        _data.photosBytes.Clear();
        _data.sprites.Clear();

        for (int i = 0; i < photos.Length; i++)
        {
            _data.photosBytes.Add(photos[i]);
            var tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            tex.LoadImage(Convert.FromBase64String(photos[i]));
            tex.Apply();
            var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            _data.sprites.Add(sprite);
        }

        for (int i = 0; i < _data.outfitsSO.Length; i++)
        {
            _data.outfitsSO[i].RestoreOutfitsValue(outfits);
        }
    }
}