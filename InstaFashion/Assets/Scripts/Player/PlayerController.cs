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
        if (input_test)
        {
            float normalizedTime = anim[0].GetCurrentAnimatorStateInfo(0).normalizedTime;
            int hashTemp = anim[0].GetCurrentAnimatorStateInfo(0).fullPathHash;
            anim[0].Play(hashTemp, 0, normalizedTime);
            anim[1].Play(hashTemp, 0, normalizedTime);
            anim[2].Play(hashTemp, 0, normalizedTime);
        }

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

    //public void 

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