using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_SpiderColonel : IState<Spider>
{
    readonly float CHASABLE_DISTANCE = 15;
    public void Initialize(Spider owner)
    {
    }

    public void Execute(Spider owner)
    {
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > CHASABLE_DISTANCE)
        {
            owner.m_StateContext.ChangeState(new ReturnToInitialBase_SpiderColonel());
        }
        else //追いかける
        {
            owner.transform.position = Vector3.MoveTowards(
                                                    owner.transform.position, 
                                                    owner.player.transform.position, 
                                                    owner.getSpeed);
        }
    }

    public void Terminate(Spider owner)
    {
    }
}
