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
    public bool tutorial = true;

    [Header("Inventory Data")]
    public OutfitSO[] outfitsSO;

    public List<string> photosBytes = new List<string>();
    public List<Sprite> sprites = new List<Sprite>();

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

    public void SavePhotos(byte[] _bytes)
    {
        string imageString = Convert.ToBase64String(_bytes);
        photosBytes.Add(imageString);

        //var tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        //tex.LoadImage(_bytes);
        //var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
    }

    public Sprite GetPhoto()
    {
        var tex = new Texture2D(1, 1, TextureFormat.ARGB32, false); // note that the size is overridden
        tex.LoadImage(Convert.FromBase64String(photosBytes[0]));
        tex.Apply();
        var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
        return sprite;
    }

}