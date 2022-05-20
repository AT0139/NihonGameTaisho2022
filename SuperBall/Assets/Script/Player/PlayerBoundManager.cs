using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBoundManager : MonoBehaviour
{
    [SerializeField] float sideBoundCor; //横バウンド時Y+方向補正値
    BlockVariable blockVariable;
    new Rigidbody2D rigidbody2D;

    float boundPower;
    int stayGroundCount = 0;
    const int STAY_GROUND_MAX = 10;
    bool isBound = true;
    int boundCnt = 0;
    const int BOUND_COUNT_MAX = 10;

    GameObject[] groundObj = null;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        //タイルマップの子オブジェクト取得
        groundObj = GetChildrens(GameObject.Find("Tilemap"));
    }

    private void Update()
    {
       // Debug.Log(isBound);
        if(!isBound)
        {
            boundCnt++;
            if(boundCnt >= BOUND_COUNT_MAX)
            {
                isBound = true;
                boundCnt = 0;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bound")
        {
            if (isBound)
            {
                //バウンド
                GroundBound(collision);
                isBound = false;
            }
        }
    }

    GameObject GetNearObject(Vector3 pos)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in groundObj)
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, pos);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }
        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }

    void GroundBound(Collision2D collision)
    {
        //衝突位置取得
        foreach (ContactPoint2D contactPoint in collision.contacts)
        {
            if (collision.gameObject.name != "Tilemap")
            {
                //衝突がタイルマップじゃなかったら衝突オブジェクトでリソースを探す
                blockVariable = Resources.Load<BlockVariable>(collision.gameObject.name);
                if (blockVariable == null)
                {
                    Debug.LogError("プレハブの名前とBoundPowerの名前を一致させてください");
                }
            }
            else
            {
                //タイルマップだったら1番近いオブジェクトでリソースを探す
                GameObject ground = GetNearObject(contactPoint.point);

                //跳ねないオブジェクト
                if (ground.tag == "GroundNotBound")
                {
                    return;
                }

                //タグで反発力取得
                blockVariable = Resources.Load<BlockVariable>(ground.tag);
                if (blockVariable == null)
                {
                    Debug.LogError("タグの名前とBoundPowerの名前を一致させてください");
                }
            }
            boundPower = blockVariable.boundPower;

            //プレイヤーのローカル座標に変換
            Vector2 localPoint = transform.InverseTransformPoint(contactPoint.point);

            //Debug.Log(localPoint);

           
            float boundAddition = Mathf.Abs(rigidbody2D.velocity.y * 0.5f);

            //上方向
            if (localPoint.y <= -0.25)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, boundPower + boundAddition);
            }
            //下方向
            else if (localPoint.y >= 0.25f)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -boundPower * 0.5f);
            }
            else if (localPoint.x >= 0.25f)
            {
                if (this.gameObject.transform.localScale.x >= 0)
                {
                    //右方向
                    rigidbody2D.velocity = new Vector2(-boundPower,  sideBoundCor);
                }
                else if (this.gameObject.transform.localScale.x <= 0)
                {
                    //左方向
                    rigidbody2D.velocity = new Vector2(boundPower,  sideBoundCor);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bound")
        {
            if (isBound)
            {
                //バウンド
                GroundBound(collision);
                isBound = false;
            }

            //    foreach (ContactPoint2D contactPoint in collision.contacts)
            //    {
            //        Vector2 localPoint = transform.InverseTransformPoint(contactPoint.point);
            //        地面にずっと接している場合(バグ)
            //        上方向
            //        if (localPoint.y <= -0.15)
            //        {
            //            stayGroundCount++;
            //        }
            //    }
            //}

            //if (stayGroundCount >= STAY_GROUND_MAX)
            //{
            //    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 10);
            //    stayGroundCount = 0;
            //}
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        stayGroundCount = 0;
    }

    GameObject[] GetChildrens(GameObject parent)
    {
        //返す配列作成
        GameObject[] childrens = new GameObject[parent.transform.childCount];
        //子オブジェクト分のループ
        for(int i=0;i< childrens.Length;i++)
        {
            childrens[i] = parent.transform.GetChild(i).gameObject;
        }

        return childrens;
    }
}