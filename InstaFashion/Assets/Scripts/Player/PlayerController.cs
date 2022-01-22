using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    
    [SerializeField]
    private Animator anim;

    //Inputs
    public Vector2 input_walk { get; private set; }

    //States
    public readonly IdleState idleState = new IdleState();
    public readonly WalkState walkState = new WalkState();
    private BaseState currentState;

    public void Start()
    {
        InitializeStates();
        SwitchState(idleState);
    }

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

    private void Update()
    {
        currentState?.UpdateState();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdateState();
    }

    #region InputMethods
    public void Input_Move(InputAction.CallbackContext _value)
    {
        input_walk = _value.ReadValue<Vector2>();
        Debug.Log("Andou: " + input_walk);
    }
    #endregion
}