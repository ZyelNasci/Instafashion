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
        //if (input_save)
        //{
        //    input_save = false;
        //    SaveSystem.Instance.SaveGame();
        //}
        //if (input_load)
        //{
        //    input_load = false;
        //    SaveSystem.Instance.LoadGame();
        //}
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

    public void LoadPlayerSettings(GameData _data)
    {
        int numb = 0;
        for (int i = 0; i < _data.outfitsSO.Length; i++)
        {
            Outfit[] temp = _data.outfitsSO[i].outfits;
            for (int j = 0; j < temp.Length; j++)
            {
                if (temp[j].selected)
                {                    
                    Debug.Log("name: " + temp[j].name);
                    SetClotheOutfit(temp[j]);
                }
                Debug.Log("Number2: " + numb.ToString());
            }
        }
    }
    public override void SetClotheOutfit(Outfit _newOutfit, bool _inEditor = false)
    {
        //base.SetClotheOutfit(_newOutfit, _inEditor);

        int index = (int)_newOutfit.myType;
        spRender[index].material.SetColor("_ColorMask", _newOutfit.itemColor);
        anim[index].runtimeAnimatorController = _newOutfit.animator;
        SetAnimatorTime();

        if (outfitInfos[index] != null)
            outfitInfos[index].selected = false;

        _newOutfit.selected = true;
        outfitInfos[index] = _newOutfit;
        Debug.Log("Trocou: ");
    }

    public void SetSkinColor(Color _color)
    {
        spRender[0].material.SetColor("_ColorMask", _color);
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