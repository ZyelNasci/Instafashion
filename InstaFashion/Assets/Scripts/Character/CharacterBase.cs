using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBase : MonoBehaviour
{
    #region Variables
    [Header("Character Components")]
    [SerializeField]
    protected Rigidbody2D body;
    [SerializeField]
    protected Animator[] anim;
    [SerializeField]
    protected SpriteRenderer[] spRender;
    public float speed { get; protected set; } = 4;

    //Inputs
    public Vector2 input_walk { get; protected set; }

    //States
    public readonly IdleState idleState = new IdleState();
    public readonly WalkState walkState = new WalkState();
    public readonly InteractState interactState = new InteractState();
    protected BaseState currentState;
    #endregion

    #region Unity Functions

    public void Awake()
    {
        InitializeStates();
    }

    public virtual void Update()
    {
        currentState?.UpdateState();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdateState();
    }
    #endregion

    #region Outfit_Methods
    public virtual void BuyingOutfit(Outfit _newOutfit)
    {

    }
    public virtual void SetClotheOutfit(Outfit _newOutfit, bool _inEditor = false)
    {
        int index = (int)_newOutfit.myType;
        if(!_inEditor)
            spRender[index].material.SetColor("_ColorMask", _newOutfit.itemColor);
        anim[index].runtimeAnimatorController = _newOutfit.animator;
        SetAnimatorTime();
    }

    public void SetAnimatorTime()
    {
        float normalizedTime = anim[0].GetCurrentAnimatorStateInfo(0).normalizedTime;
        int hashTemp = anim[0].GetCurrentAnimatorStateInfo(0).fullPathHash;

        for (int i = 0; i < anim.Length; i++)
            anim[i].Play(hashTemp, 0, normalizedTime);
    }
    #endregion

    private void InitializeStates()
    {
        idleState.InitialiseState(this, anim);
        walkState.InitializeState(this, anim, body);
        interactState.InitializeState(this, anim);
    }

    public void SwitchState(BaseState newState)
    {
        currentState?.ExitState();
        newState.EnterState();

        currentState = newState;
    }
}