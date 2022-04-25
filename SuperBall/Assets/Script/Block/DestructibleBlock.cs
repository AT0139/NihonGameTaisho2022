using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    readonly private float PLAYER_MAGNITUDE = 5;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "ball 1")
        {
            Debug.Log("DestructibleBlockに衝突");

            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > PLAYER_MAGNITUDE)
            {
                Debug.Log("破壊");
                Destroy(gameObject);
                Instantiate(/*(GameObject)Resources.Load("Explosion001")*/explosion, transform.position, new Quaternion());
            }
        }
    }
}
