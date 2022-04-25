using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------------
//  バブルエフェクトの設定
//  
//----------------------------------------------------------------------

public class S_EffectBubble : MonoBehaviour
{
    public ParticleSystem BubbleEffect;
    public float NomalizeValue = 5.0f;


    private ParticleSystem[] childrenBubble;
    private ParticleSystem particle;

    Rigidbody2D rigidbody2D;
    float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        particle = Instantiate(BubbleEffect,transform.position,Quaternion.identity);

        childrenBubble = particle.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        movementSpeed = rigidbody2D.velocity.magnitude;

        foreach(var x in childrenBubble)
        {
            var em = x.emission;
            em.enabled = true;

            em.rateOverTime = movementSpeed / NomalizeValue - 1.0f;
        }

        particle.transform.position = this.transform.position;

        
    }
}
