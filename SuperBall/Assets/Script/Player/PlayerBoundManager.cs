using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBoundManager : MonoBehaviour
{
    [SerializeField] float sideBoundCor; //横バウンド時Y+方向補正値
    BlockVariable blockVariable;
    float boundPower;
    //List<GameObject> colList = new List<GameObject>();
    new Rigidbody2D rigidbody2D;


    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Groundタグとぶつかったとき
        if (collision.gameObject.tag == "Ground")
        {
            //反発力取得
            blockVariable = Resources.Load<BlockVariable>("BoundPowerTest");
            boundPower = blockVariable.boundPower;
            
            //colList.Add(collision.gameObject);

            //衝突位置取得
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                //ローカル座標に変換
                Vector2 localPoint = transform.InverseTransformPoint(contactPoint.point);

                //Debug.Log(localPoint);

                //上方向
                if (localPoint.y <= -0.15)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, boundPower);
                }
                //下方向
                else if (localPoint.y >= 0.15f)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -boundPower);
                }
                else if (localPoint.x >= 0.15f)
                {
                    if (this.gameObject.transform.localScale.x >= 0)
                    {
                        //右方向
                        rigidbody2D.velocity = new Vector2(-boundPower, rigidbody2D.velocity.y + sideBoundCor);
                    }
                    else if (this.gameObject.transform.localScale.x <= 0)
                    {
                        //左方向
                        rigidbody2D.velocity = new Vector2(boundPower, rigidbody2D.velocity.y + sideBoundCor);
                    }
                }
            }
        }
    }
}
