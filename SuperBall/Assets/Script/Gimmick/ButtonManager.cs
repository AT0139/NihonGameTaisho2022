using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  製作者：柳澤優太

public class ButtonManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;            //    スプライト変えるため

    [SerializeField] GameObject DestroyObject;      //　破壊するオブジェクト

    public Sprite buttonSprite; //スプライト


    public GameObject particleObject;

    private bool IsCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //  すでに一回あたっていたら
        if (IsCollision) return;

        //プレイヤーならスプライト切り替え
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.sprite = buttonSprite;

            //  パーティクル生成
            Instantiate(particleObject, DestroyObject.transform.position, Quaternion.identity);

            //  オブジェクト破壊
            Destroy(DestroyObject);


            IsCollision = true;
        }
    }
}
