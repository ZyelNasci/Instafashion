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
    
    protected Outfit[] outfitInfos = new Outfit[4];

    private bool input_money = false;
    #endregion
    public override void Update()
    {
        base.Update();
        if(input_money == true)
        {
            GameController.Instance.UpdateMoney(30);
            input_money = false;
        }        
    }

    /// <summary>
    /// Set all player settings
    /// </summary>
    /// <param name="_data"></param>
    public void LoadPlayerSettings(GameData _data)
    {
        int numb = 0;
        SetSkinColor(_data.colorSkin);
        for (int i = 0; i < _data.outfitsSO.Length; i++)
        {
            Outfit[] temp = _data.outfitsSO[i].outfits;
            for (int j = 0; j < temp.Length; j++)
            {
                if (temp[j].selected)
                {                                        
                    SetClotheOutfit(temp[j]);
                }                
            }
        }
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

        int index = (int)_newOutfit.myType;
        if (outfitInfos[index] != null)
            outfitInfos[index].selected = false;

        _newOutfit.selected = true;
        outfitInfos[index] = _newOutfit;        
    }

    public void SetSkinColor(Color _color)
    {
        spRender[0].material.SetColor("_ColorMask", _color);
    }

    /// <summary>
    /// Returns the total popularity of the clothes
    /// </summary>
    /// <returns></returns>
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
    #endregion


    #region InputMethods
    public void Input_Move(InputAction.CallbackContext _value)
    {
        input_walk = _value.ReadValue<Vector2>();        
    }

    public void Input_GainMoney(InputAction.CallbackContext _value)
    {        
        input_money = _value.ReadValueAsButton();
    }

    public void Input_Load(InputAction.CallbackContext _value)
    {
        //input_walk = _value.ReadValue<Vector2>();        
    }
    #endregion
}