using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    // 残機
    [SerializeField]
    private int m_PlayerStock;

    // 体力
    [SerializeField]
    private int m_PlayerLifeMax;

    private int m_PlayerLife;

    // StockGaugeスクリプト(残機)
    [SerializeField]
    private StockGauge playerStock;

    // LifeManagerスクリプト(体力)
    [SerializeField]
    private LifeManager lifeManager;

    private PlayerTouchCheckPoint checkPoint;

    void Start()
    {
        checkPoint = GetComponent<PlayerTouchCheckPoint>();

        // 表示部分に残機のセット
        playerStock.SetStockGauge(m_PlayerStock);

        m_PlayerLife = m_PlayerLifeMax;

    }

    void Update()
    {
        // 残機が0未満になったら
        if (m_PlayerStock < 0)
        {
            // ゲームオーバー
            SceneManager.LoadScene("GameOverScene");
        }
    }


    // ダメージ処理
    public void GetDamege(int damege)
    {
        // ダメージ
        m_PlayerLife -= damege;

        // 体力が0以下になったら
        if(m_PlayerLife <= 0)
        {
            // 残機を1減らす
            m_PlayerStock -= 1;

            // 体力を最大値に戻す
            m_PlayerLife = m_PlayerLifeMax;

            // ストックを減らす
            playerStock.StockDamege(1);

            // リスポーン地点に移動させる
            checkPoint.SetCheckPoint();
            
        }
        // ダメージ後体力が残ってたら
        else
        {
            // ダメージ表示
            lifeManager.ShowLife(m_PlayerLife);
        }
    }

    // 回復処理
    public void GetRecovery(int recovery)
    {
        // ダメージ
        m_PlayerLife += recovery;

        // 最大値を超えたら最大値に戻す
        if(m_PlayerLife > m_PlayerLifeMax)
        {
            m_PlayerLife = m_PlayerLifeMax;
        }

        // ライフ表示
        lifeManager.ShowLife(m_PlayerLife);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // エネミーに接触したら
        if (other.gameObject.CompareTag("Enemy"))
        {
            int enemystate = other.gameObject.GetComponent<EnemyManager>().GetEnemyState();

            // エネミーが攻撃状態だったら
            if (enemystate == 1)
            {
                GetDamege(1);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // チェックポイントに接触したら
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            // 回復処理
            GetRecovery(m_PlayerLifeMax);
        }
    }
}
