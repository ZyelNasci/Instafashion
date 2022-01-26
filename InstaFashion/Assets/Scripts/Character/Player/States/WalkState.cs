using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkState : BaseState
{
    private Rigidbody2D body;
    

    public void InitializeState(CharacterBase _player, Animator[] _anim, Rigidbody2D _body)
    {
        player  = _player;
        anim    = _anim;
        body    = _body;
    }

    public override void EnterState()
    {
        for (int i = 0; i < anim.Length; i++)        
            anim[i].SetBool("walk", true);
    }

    public override void ExitState()
    {
        body.velocity = Vector2.zero;        
        for (int i = 0; i < anim.Length; i++)        
            anim[i].SetBool("walk", false);
        
    }

    public override void UpdateState()
    {
        if (player.input_walk == Vector2.zero)
        {
            player.SwitchState(player.idleState);
            return;
        }

        SetAnim();

        Vector2 velocity = player.input_walk;
        body.velocity = velocity * player.speed;
    }

    private Vector2 currentDirection;
    public void SetAnim()
    {
        if(currentDirection != player.input_walk)
        {
            for (int i = 0; i < anim.Length; i++)
            {
                anim[i].SetFloat("velX", Mathf.RoundToInt(player.input_walk.x));
                anim[i].SetFloat("velY", Mathf.RoundToInt(player.input_walk.y));
            }
            currentDirection = player.input_walk;
        }  
    }

    public override void FixedUpdateState()
    {

    }
}