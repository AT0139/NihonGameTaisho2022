using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchCheckPoint : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool PlayerDeath;

    private GameObject CheckPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        PlayerDeath = false;

        CheckPoint = null;
    }

    void Update()
    {

        // プレイヤーが死亡判定になったとき
        if (PlayerDeath)
        {
            // チェックポイントに触れたあとのとき
            if(CheckPoint != null)
            {
                rb.transform.position = CheckPoint.transform.position;
                rb.velocity = new Vector3(0.0f,0.0f, 0.0f);
            }
            // チェックポイントに触れてないとき
            else
            {
                // 現在のスタート地点に過ぎないので後々チェックポイントのように指定することも可
                rb.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            
            // 判定初期化
            PlayerDeath = false;
        }
        
    }

    // 侵入判定
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            this.CheckPoint = other.gameObject;
        }

    }

    public void SetCheckPoint()
    {
        PlayerDeath = true;
    }
}
