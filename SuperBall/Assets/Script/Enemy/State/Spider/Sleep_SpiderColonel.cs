using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep_SpiderColonel : IState<Spider>
{
    public void Initialize(Spider owner)
    {
    }

    public void Execute(Spider owner)
    {
        foreach(Transform nets in owner.transform)
            if(nets.GetComponent<SpiderNet>().onNet)
            {
                owner.m_StateContext.ChangeState(new Chase_SpiderColonel());
            }
    }

    public void Terminate(Spider owner)
    {
    }
}
