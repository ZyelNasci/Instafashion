using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkState : BaseState
{
    public void InitializeState(PlayerController _player, Animator[] _anim)
    {
        player = _player;
        anim = _anim;
    }

    public override void EnterState()
    {
        for (int i = 0; i < anim.Length; i++)        
            anim[i].SetBool("walk", true);
    }

    public override void ExitState()
    {
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
        
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetFloat("velX", player.input_walk.x);
            anim[i].SetFloat("velY", player.input_walk.y);
        }        
    }

    public override void FixedUpdateState()
    {
        
    }
}