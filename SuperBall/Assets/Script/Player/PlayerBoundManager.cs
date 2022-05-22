using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBoundManager : MonoBehaviour
{
    [SerializeField] HitStopManager hitStopManager;

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

    GameObject nearobj = null;
    GameObject secondobj = null;

    float hitStopCount;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        //タイルマップの子オブジェクト取得
        groundObj = GetChildrens(GameObject.Find("Tilemap"));
    }

    private void Update()
    {
        //Debug.Log(rigidbody2D.velocity.magnitude);
        if (!isBound)
        {
            boundCnt++;
            if (boundCnt >= BOUND_COUNT_MAX)
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

    void GetNearObject(Vector3 pos)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        float secondDis = 0;        //2番目に近いオブジェクトの距離

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in groundObj)
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, pos);

            if(obs.tag != "GroundNotBound")
            {
                tmpDis -= 1;
            }

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {       
                secondobj = nearobj;
                secondDis = nearDis;

                nearDis = tmpDis;
                nearobj = obs;
            }
            //1番近いオブジェクトより遠かったら2番目とも比較する
            else if (secondDis == 0 || nearDis > tmpDis)
            {
                secondDis = tmpDis;
                secondobj = obs;
            }
        }
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
                GetNearObject(contactPoint.point);

                //跳ねないオブジェクト
                if (nearobj.tag == "GroundNotBound")
                {
                    if (secondobj.tag == "GroundNotBound")
                    {
                        return;
                    }
                    else
                    {
                        //nearobj = secondobj;
                    }
                }

                //タグで反発力取得
                blockVariable = Resources.Load<BlockVariable>(nearobj.tag);
                if (blockVariable == null)
                {
                    Debug.LogError("タグの名前とBoundPowerの名前を一致させてください");
                }
            }

            boundPower = blockVariable.boundPower;

            //プレイヤーのローカル座標に変換
            Vector2 localPoint = transform.InverseTransformPoint(contactPoint.point);

            Debug.Log(boundPower);


            float boundAddition = Mathf.Abs(rigidbody2D.velocity.y * 0.5f);

            //上方向
            if (localPoint.y <= -0.25)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, boundPower + boundAddition);
                HitStopDecision();
            }
            //下方向
            else if (localPoint.y >= 0.25f)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -boundPower * 0.5f);
                HitStopDecision();
            }
            else if (localPoint.x >= 0.25f)
            {
                if (this.gameObject.transform.localScale.x >= 0)
                {
                    //右方向
                    rigidbody2D.velocity = new Vector2(-boundPower, sideBoundCor);
                    HitStopDecision();
                }
                else if (this.gameObject.transform.localScale.x <= 0)
                {
                    //左方向
                    rigidbody2D.velocity = new Vector2(boundPower, sideBoundCor);
                    HitStopDecision();
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
        for (int i = 0; i < childrens.Length; i++)
        {
            childrens[i] = parent.transform.GetChild(i).gameObject;
        }

        return childrens;
    }

    void HitStopDecision()
    {
        if (rigidbody2D.velocity.magnitude >= 30.0f )
        {
            //hitStopManager.SetHitStop(0.1f);
        }
    }
}