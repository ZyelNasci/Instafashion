using System;
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

    public List<string> photosBytes = new List<string>();
    public List<Sprite> sprites = new List<Sprite>();

#if UNITY_EDITOR

    public bool test;
    public bool clear;
    private void OnValidate()
    {
        if (test)
        {
            //sprites.Add(GetPhoto());
        }
        if (clear)
        {
            clear = false;
            photosBytes.Clear();
            sprites.Clear();
        }
    }

#endif
    public void SavePhotos(byte[] _bytes)
    {
        string imageString = Convert.ToBase64String(_bytes);
        photosBytes.Add(imageString);

        //var tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        //tex.LoadImage(_bytes);
        //var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
    }

    public void ResetGameData()
    {
        Money = 0;
        playerName = "";
        totalLikes = 0;
        totalFollowers = 0;
        tutorial = true;
        photosBytes.Clear();
        sprites.Clear();

        for (int i = 0; i < outfitsSO.Length; i++)
        {
            outfitsSO[i].ResetSO();
        }
    }

}