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
    #endregion

    #region Unity Functions

    #endregion

    #region Outfit_Methods
    public override void BuyingOutfit(Outfit _newOutfit)
    {
        SetClotheOutfit(_newOutfit);
        inventory.AddNewInventoryOutfit(_newOutfit);
    }
    #endregion

    #region InputMethods
    public void Input_Move(InputAction.CallbackContext _value)
    {
        input_walk = _value.ReadValue<Vector2>();        
    }
    #endregion
}