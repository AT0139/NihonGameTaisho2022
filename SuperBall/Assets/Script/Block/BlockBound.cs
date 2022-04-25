using UnityEngine;

public class BlockBound : MonoBehaviour
{
    [SerializeField] float boundPower;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (transform.rotation.z == 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, boundPower * 100));
            }
            else
            {
                if (transform.position.x >= collision.transform.position.x)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-boundPower * 100, 1000));
                }
                else
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(boundPower * 100, 1000));
                }
            }
        }
    }
}
