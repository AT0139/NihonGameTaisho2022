using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockGauge : MonoBehaviour
{
    //　残機プレハブ
    [SerializeField]
    private GameObject stockObj;

    // 残機全削除＆残機分作成
    public void SetStockGauge(int life)
    {
        // 残機を一旦全削除
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        // 現在の残機数分のライフゲージを作成
        for (int i = 0; i < life; i++)
        {
            Instantiate(stockObj, transform);
        }
    }
    //　ダメージ分だけ削除
    public void StockDamege(int damage)
    {
        for (int i = 0; i < damage; i++)
        {
            // 残機を削除
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}