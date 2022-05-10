using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToInitialBase_SpiderColonel : IState<Spider>
{
    public void Initialize(Spider owner)
    {
        Debug.Log("initialize return to base");
    }

    public void Execute(Spider owner)
    {
       // if(owner.transform.position != initialPosition)
            owner.transform.position = Vector3.MoveTowards(
                                                owner.transform.position,
                                                owner.GetComponent<Spider>().initialPosition,
                                                owner.GetComponent<Spider>().getSpeed);
    }

    public void Terminate(Spider owner)
    {
    }
}
