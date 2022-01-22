using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkState : BaseState
{
    public void InitializeState(PlayerController _player, Animator _anim)
    {
        player = _player;
        anim = _anim;
    }

    public override void EnterState()
    {
        anim.SetBool("walk", true);
    }

    public override void ExitState()
    {
        anim.SetBool("walk", false);
    }

    public override void UpdateState()
    {
        if (player.input_walk == Vector2.zero)
        {
            player.SwitchState(player.idleState);
            return;
        }

        anim.SetFloat("velX", player.input_walk.x);
        anim.SetFloat("velY", player.input_walk.y);
    }

    public override void FixedUpdateState()
    {
        
    }
}