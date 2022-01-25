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

    public Outfit[] Hair;
    public Outfit[] Clothes;
    public Outfit[] Accessories;

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
            switch (_data.outfitsSO[i].type)
            {
                case OutfitType.Accessories:
                    Accessories = _data.outfitsSO[i].outfits;
                    break;
                case OutfitType.Hairs:
                    Hair = _data.outfitsSO[i].outfits;
                    break;
                case OutfitType.Clothes:
                    Clothes = _data.outfitsSO[i].outfits;
                    break;
            }
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

        for (int i = 0; i < _data.outfitsSO.Length; i++)
        {
            switch (_data.outfitsSO[i].type)
            {
                case OutfitType.Accessories:
                    _data.outfitsSO[i].RestoreOutfitsValue(Accessories);
                    break;
                case OutfitType.Hairs:
                    _data.outfitsSO[i].RestoreOutfitsValue(Hair);
                    break;
                case OutfitType.Clothes:
                    _data.outfitsSO[i].RestoreOutfitsValue(Clothes);
                    break;
            }
        }

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

    }
}