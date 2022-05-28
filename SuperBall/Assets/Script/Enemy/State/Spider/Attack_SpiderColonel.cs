using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_SpiderColonel : IState<Spider>
{
    public void Initialize(Spider owner)
    {
        owner.enemyAnimeState.SetAnimation(0, owner.ATTACK_ANIMATION, true);
    }

    public void Execute(Spider owner)
    {
        owner.player.GetComponent<PlayerLife>().GetDamege(1);
        owner.m_StateContext.ChangeState(new Chase_SpiderColonel());
    }

    public void Terminate(Spider owner)
    {
    }
}
