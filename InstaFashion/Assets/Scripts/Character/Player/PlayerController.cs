using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterBase
{
    #region Variables
    [Header("Player Components")]
    [SerializeField]
    private InventoryManager inventory;

    [SerializeField]
    protected Outfit[] outfitInfos = new Outfit[4];

    private bool input_save = false;
    private bool input_load = false;
    #endregion

    #region Unity Functions
    private void Start()
    {
        //RandomizeOutfit();
    }
    #endregion

    public override void Update()
    {
        base.Update();
        if (input_save)
        {
            input_save = false;
            SaveSystem.Instance.SaveGame();
        }
        if (input_load)
        {
            input_load = false;
            SaveSystem.Instance.LoadGame();
        }
    }

    public int GetTotalPopularityOutift()
    {
        int total = 0;
        for (int i = 0; i < outfitInfos.Length; i++)
        {
            if (outfitInfos[i] != null)
            {
                total += outfitInfos[i].currentPopularityStar;
                if (outfitInfos[i].currentPopularityStar > 0)
                    outfitInfos[i].currentPopularityStar--;
            }                
        }
        return total;
    }

    #region Outfit_Methods
    public override void BuyingOutfit(Outfit _newOutfit)
    {
        SetClotheOutfit(_newOutfit);
        inventory.AddNewInventoryOutfit(_newOutfit);
    }

    public override void SetClotheOutfit(Outfit _newOutfit, bool _inEditor = false)
    {
        base.SetClotheOutfit(_newOutfit, _inEditor);

        _newOutfit.selected = true;

        if (outfitInfos[(int)_newOutfit.myType] != null)
            outfitInfos[(int)_newOutfit.myType].selected = false;

        outfitInfos[(int)_newOutfit.myType] = _newOutfit;
    }

    public void SetSkinColor(Color _color)
    {
        spRender[0].material.SetColor("_ColorMask", _color);
    }

    public void RandomizeOutfit()
    {
        OutfitSO clothes = Resources.Load<OutfitSO>("Scriptables/ClothesSO");
        OutfitSO hairs = Resources.Load<OutfitSO>("Scriptables/HairSO");
        OutfitSO Accessories = Resources.Load<OutfitSO>("Scriptables/AccessoriesSO");

        int clothesIndex = Random.Range(0, clothes.outfits.Length);
        int hairIndex = Random.Range(0, hairs.outfits.Length);
        int accessoriesIndex = Random.Range(0, Accessories.outfits.Length);

        SetClotheOutfit(clothes.outfits[clothesIndex]);
        SetClotheOutfit(hairs.outfits[hairIndex]);
        SetClotheOutfit(Accessories.outfits[accessoriesIndex]);

        //spRender[0].material.SetColor("_ColorMask", colorSkin.Evaluate(Random.Range(0f, 1f)));
    }
    #endregion

    #region InputMethods
    public void Input_Move(InputAction.CallbackContext _value)
    {
        input_walk = _value.ReadValue<Vector2>();        
    }

    public void Input_Save(InputAction.CallbackContext _value)
    {
        //input_walk = _value.ReadValue<Vector2>();        
        input_save = _value.ReadValueAsButton();
    }

    public void Input_Load(InputAction.CallbackContext _value)
    {
        //input_walk = _value.ReadValue<Vector2>();
        
        input_load = _value.ReadValueAsButton();
    }
    #endregion
}