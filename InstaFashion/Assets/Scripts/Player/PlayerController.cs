using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Components")]    
    [SerializeField]
    private Animator[] anim;
    [SerializeField]
    private SpriteRenderer[] spRender;

    //Inputs
    public Vector2 input_walk { get; private set; }
    public bool input_test { get; private set; }

    //States
    public readonly IdleState idleState = new IdleState();
    public readonly WalkState walkState = new WalkState();
    private BaseState currentState;

    private OutiftManager outfitManager;
    #endregion

    #region Unity Functions

    public void Start()
    {
        outfitManager = new OutiftManager(this);

        InitializeStates();
        SwitchState(idleState);
    }

    private void Update()
    {
        currentState?.UpdateState();
        input_test = false;
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdateState();
    }
    #endregion

    private void InitializeStates()
    {
        idleState.InitialiseState(this, anim);
        walkState.InitializeState(this, anim);
    }

    public void SwitchState(BaseState newState)
    {
        currentState?.ExitState();
        newState.EnterState();

        currentState = newState;
    }

    #region Outfit_Methods
    public void SetClotheOutfit(Outfit _newOutfit)
    {
        int index = (int)_newOutfit.myType + 1;
        spRender[index].material.SetColor("_ColorMask", _newOutfit.itemColor);
        anim[index].runtimeAnimatorController = _newOutfit.animator;
        SetAnimatorTime();
    }

    public void SetAnimatorTime()
    {
        float normalizedTime = anim[0].GetCurrentAnimatorStateInfo(0).normalizedTime;
        int hashTemp = anim[0].GetCurrentAnimatorStateInfo(0).fullPathHash;
        anim[0].Play(hashTemp, 0, normalizedTime);
        anim[1].Play(hashTemp, 0, normalizedTime);
        anim[2].Play(hashTemp, 0, normalizedTime);
    }
    #endregion

    #region InputMethods
    public void Input_Move(InputAction.CallbackContext _value)
    {
        input_walk = _value.ReadValue<Vector2>();
        Debug.Log("Andou: " + input_walk);
    }

    public void Input_Test(InputAction.CallbackContext _value)
    {
        input_test = _value.ReadValueAsButton();
    }
    #endregion
}