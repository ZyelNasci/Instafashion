using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : BaseState
{

    public void InitializeState(CharacterBase _player, Animator []_anim) 
    {
        player = _player;
        anim = _anim;
    }
    public override void EnterState()
    {
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetFloat("velX", 0);
            anim[i].SetFloat("velY", -1);
        }
    }
    public override void ExitState()
    {

    }
    public override void UpdateState()
    {
        if(player.input_walk != Vector2.zero)
        {
            for (int i = 0; i < anim.Length; i++)
            {
                anim[i].SetFloat("velX", player.input_walk.x);
                anim[i].SetFloat("velY", player.input_walk.y);
            }
        }
    }
    public override void FixedUpdateState()
    {
        
    }    
}