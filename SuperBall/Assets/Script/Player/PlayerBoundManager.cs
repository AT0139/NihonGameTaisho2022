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
        if (collision.gameObject.tag == "Tilemap")
        {
            //衝突位置取得
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                //バウンド
                GroundBound(GetNearObject(contactPoint.point),contactPoint);
            }
        }
    }

    GameObject GetNearObject(Vector3 pos)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag("Ground"))
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

    void GroundBound(GameObject ground, ContactPoint2D contactPoint)
    {
        //反発力取得
        blockVariable = Resources.Load<BlockVariable>(ground.name);
        if(blockVariable == null)
        {
            Debug.LogError("プレハブの名前とBoundPowerの名前を一致させてください");
        }
        boundPower = blockVariable.boundPower;

        //プレイヤーのローカル座標に変換
        Vector2 localPoint = transform.InverseTransformPoint(contactPoint.point);

        //Debug.Log(ground.name);

        //上方向
        if (localPoint.y <= -0.15)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, boundPower);
        }
        //下方向
        else if (localPoint.y >= 0.15f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -boundPower * 0.5f);
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