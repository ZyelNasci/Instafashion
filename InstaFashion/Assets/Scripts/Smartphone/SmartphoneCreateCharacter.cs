using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SmartphoneCreateCharacter : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private SmartphoneManager manager;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Gradient color;

    private SaveSystem save;

    public List<Outfit> hairs;
    public List<Outfit> clothes;
    public List<Outfit> accessories;

    private int hairIndex;
    private int clothesIndex;
    private int accessoriesIndex;
    private float skinIndex = 0.5f;
    
    private string name;

    public void InitializeScreen()
    {
        save = SaveSystem.Instance;
        OutfitSO hairsTemp          = Resources.Load<OutfitSO>("Scriptables/HairSO");
        OutfitSO clothesTemp        = Resources.Load<OutfitSO>("Scriptables/ClothesSO");
        OutfitSO accessoriesTemp    = Resources.Load<OutfitSO>("Scriptables/AccessoriesSO");

        hairs       = hairsTemp.GetOnlyInventoryType(InventoryType.PlayerInventory);
        clothes     = clothesTemp.GetOnlyInventoryType(InventoryType.PlayerInventory);
        accessories = accessoriesTemp.GetOnlyInventoryType(InventoryType.PlayerInventory);

        player.SetSkinColor(color.Evaluate(skinIndex));
        player.SetClotheOutfit(hairs[0]);
        player.SetClotheOutfit(clothes[0]);
        player.SetClotheOutfit(accessories[0]);
    }

    public void OnClick_SwitchHair(int _dir)
    {
        hairIndex += _dir;
        if (_dir > 0 && hairIndex >= hairs.Count)
            hairIndex = 0;
        else if (_dir < 0 && hairIndex < 0)
            hairIndex = hairs.Count - 1;

        player.SetClotheOutfit(hairs[hairIndex]);
    }

    public void OnClick_SwitchSkinColor(int _dir)
    {
        if(_dir > 0)
            skinIndex += 0.1f;
        else
            skinIndex -= 0.1f;

        if (skinIndex > 1)
            skinIndex = 0;
        else if (skinIndex < 0)
            skinIndex = 1;

        player.SetSkinColor(color.Evaluate(skinIndex));
    }

    public void OnClick_SwitchClothes(int _dir)
    {
        clothesIndex += _dir;

        if (_dir > 0 && clothesIndex >= clothes.Count)
            clothesIndex = 0;
        else if (_dir < 0 && clothesIndex < 0)
            clothesIndex = clothes.Count - 1;

        player.SetClotheOutfit(clothes[clothesIndex]);
    }

    public void OnClick_SwitchAccessories(int _dir)
    {
        accessoriesIndex += _dir;

        if (_dir > 0 && accessoriesIndex >= accessories.Count)
            accessoriesIndex = 0;
        else if (_dir < 0 && accessoriesIndex < 0)
            accessoriesIndex = accessories.Count - 1;

        player.SetClotheOutfit(accessories[accessoriesIndex]);
    }

    public void OnClick_CreatePerfil()
    {        
        manager.SwitchScreen(SmartphoneScreen.Camera);
    }

    public void OnInput_Setname(string _value)
    {
        name = inputField.text;
        manager.SetPerfilname(name);        
    }
}