using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //player.gameObject.GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //砂の中にいる時
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("すり抜けている");
            GameObject.Find("ball 1").GetComponent<Rigidbody2D>().drag = 50;
            
        }
    }

    //砂からでた時
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("すり抜けた");
            GameObject.Find("ball 1").GetComponent<Rigidbody2D>().drag = 0;
        }
    }
}
