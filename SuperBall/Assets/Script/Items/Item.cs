using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ball 1")
        {
            Debug.Log("Itemに衝突");
            Destroy(gameObject);
        }
    }
}
