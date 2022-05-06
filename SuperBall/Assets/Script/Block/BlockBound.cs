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
                foreach (ContactPoint2D contactPoint in collision.contacts)
                {
                    //ローカル座標に変換
                    Vector2 localPoint = transform.InverseTransformPoint(contactPoint.point);

                    //Debug.Log(localPoint);

                    //上面
                    if (localPoint.y >= 0.45f)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, boundPower * 100));
                    }
                    //下面
                    else if (localPoint.y <= -0.45f)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -boundPower * 100));
                    }
                    //右面
                    else if (localPoint.x >= 0.45f)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(boundPower * 100, 0));
                    }
                    //左面
                    else if(localPoint.x <= -0.45f)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-boundPower * 100,0));
                    }
                }
                isBound = false;
            }
        }
    }
}
