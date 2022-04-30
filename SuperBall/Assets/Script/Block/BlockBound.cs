using UnityEngine;

public class BlockBound : MonoBehaviour
{
    [SerializeField] float boundPower;

    int cnt = 0;
    int BOUND_POSSIBLE_TIME = 10;
    static bool isBound;

    private void Update()
    {
        if(!isBound)
        {
            cnt++;
            if(cnt >= BOUND_POSSIBLE_TIME)
            {
                cnt = 0;
                isBound = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isBound)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, boundPower * 100));
                isBound = false;
            }
        }
    }
}
