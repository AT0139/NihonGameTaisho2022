using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBoundManager : MonoBehaviour
{
    float boundPower;
    List<GameObject> colList = new List<GameObject>();

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            boundPower = collision.gameObject.GetComponent<BlockBound>().boundPower;

            colList.Add(collision.gameObject);

            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                //ローカル座標に変換
                Vector2 localPoint = transform.InverseTransformPoint(contactPoint.point);

                Debug.Log(localPoint);

                //上面
                if (localPoint.y >= 0.45f)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, boundPower * 100);
                }
                //下面
                else if (localPoint.y <= -0.45f)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -boundPower * 100);
                }
                //右面
                else if (localPoint.x >= 0.45f)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(boundPower * 100, 0);
                }
                //左面
                else if (localPoint.x <= -0.45f)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-boundPower * 100, 0);
                }
            }
        }
    }
}
