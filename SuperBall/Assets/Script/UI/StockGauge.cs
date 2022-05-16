using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        transform.GetChild(0).gameObject.GetComponent<RawImage>().color = new Color(255.0f, 255.0f, 255.0f, 190.0f);
        transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(50.0f, 50.0f);
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