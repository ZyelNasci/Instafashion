using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public void InitialiseState(PlayerController _player, Animator[] _anim)
    {
        player = _player;
        anim = _anim;
    }

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if (player.input_walk != Vector2.zero)
            player.SwitchState(player.walkState);
    }

    public override void FixedUpdateState()
    {
        
    }
}
