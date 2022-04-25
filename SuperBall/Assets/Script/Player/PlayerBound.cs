using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*==============================================================================
  PlayerBound
														 Author :君島朝日
														 Date   :2022/03/24
--------------------------------------------------------------------------------
反発ボタンを押しているとより強く反発するようにするスクリプト(Wallにぶつかった時のみ)
==============================================================================*/

public class PlayerBound : MonoBehaviour
{
    private Rigidbody2D rbody2D;

    public float jumpForce;
    public float BoundForce;

    private bool jumpCount = false;
    private bool BoundCount = false;

    //プレイヤーの進んでいるx方向を見る変数
    private bool PlayerDireX = true;
    //プレイヤーの進んでいるY方向を見る変数
    private bool PlayerDireY = false;


    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        //move = input.Player.Move.ReadValue<Vector2>();



        //現在の速度を取得
        Vector2 RbSp = rbody2D.GetComponent<Rigidbody2D>().velocity;

        //プレイヤのx速度が0になるまでの進んでいた方向を保存する
        if (RbSp.x > 0)
            PlayerDireX = true;
        else if (RbSp.x < 0)
            PlayerDireX = false;

        //プレイヤのy速度が0になるまでの進んでいた方向を保存する
        if (RbSp.y > 0)
            PlayerDireY = true;
        else if (RbSp.y < 0)
            PlayerDireY = false;

        // 反発ボタン(↓)が押されたか
        bool jumpswitch = false;


        //// 上入力？
        //if (move.y > 0)
        //{
        //    jumpswitch = false;
        //}
        //// 下入力？
        //else if (move.y < 0)
        //{
        //    jumpswitch = true;
        //}
        //else
        //{
        //    jumpswitch = false;
        //}

        /*
        // 地面の反発
        if (jumpswitch && this.jumpCount && RbSp.y == 0 && PlayerDireY == false)
        {
            this.rbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            jumpCount = false;
        }

        // 天井の反発
        if (jumpswitch && this.jumpCount && RbSp.y == 0 && PlayerDireY == true)
        {
            this.rbody2D.AddForce(transform.up * -jumpForce, ForceMode2D.Impulse);
            jumpCount = false;
        }
        */

        //// 壁の反発
        //if (this.BoundCount && PlayerDireX == false)
        //{
        //    this.rbody2D.AddForce(transform.right * BoundForce, ForceMode2D.Impulse);
        //    this.rbody2D.AddForce(transform.up * jumpForce * 1.1f);
        //    BoundCount = false;
        //}

        //// 壁の反発
        //if (this.BoundCount && PlayerDireX == true)
        //{
        //    this.rbody2D.AddForce(transform.right * -BoundForce, ForceMode2D.Impulse);
        //    this.rbody2D.AddForce(transform.up * jumpForce * 1.1f);
        //    BoundCount = false;
        //}

        ////physics2Dを利用する場合はこちらを使用する
        //// 地面の反発
        //if (jumpswitch && this.jumpCount)
        //{
        //    this.rbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        //    jumpCount = false;
        //}

        //// 壁の反発
        //if (jumpswitch && this.BoundCount && RbSp.x > 0)
        //{
        //    this.rbody2D.AddForce(transform.right * BoundForce, ForceMode2D.Impulse);
        //    this.rbody2D.AddForce(transform.up * jumpForce * 1.1f);
        //    BoundCount = false;
        //}

        //// 壁の反発
        //if (jumpswitch && this.BoundCount && RbSp.x < 0)
        //{
        //    this.rbody2D.AddForce(transform.right * - BoundForce, ForceMode2D.Impulse);
        //    this.rbody2D.AddForce(transform.up * jumpForce * 1.1f);
        //    BoundCount = false;
        //}




    }


    // オブジェクトと接地しているとき
    private void OnCollisionEnter2D(Collision2D other)
    {
        // 壁に触れた時
        if (other.gameObject.CompareTag("Wall"))
        {
            // 壁の反発
            if (this.BoundCount && PlayerDireX == false)
            {
                rbody2D.velocity = new Vector3(0, 0, 0);

                this.rbody2D.AddForce(transform.right * BoundForce * 1.0f, ForceMode2D.Impulse);
                //this.rbody2D.AddForce(transform.up * jumpForce * 1.1f);

                BoundCount = false;
            }

            // 壁の反発
            if (this.BoundCount && PlayerDireX == true)
            {
                rbody2D.velocity = new Vector3(0, 0, 0);

                this.rbody2D.AddForce(transform.right * -BoundForce * 1.0f, ForceMode2D.Impulse);
                //this.rbody2D.AddForce(transform.up * jumpForce * 1.1f);

                BoundCount = false;
            }
        }

    }

    // オブジェクトと接地してないとき
    private void OnCollisionExit2D()
    {
        jumpCount = false;
        //BoundCount = false;
    }


    // 範囲内の判定
    void OnTriggerEnter2D(Collider2D other)
    {
        // ジャストジャンプの範囲内
        if (other.gameObject.CompareTag("BoundRange"))
        {
            BoundCount = true;
        }
    }

    // 範囲外の判定
    void OnTriggerExit2D(Collider2D other)

    {
        // ジャストジャンプの範囲内
        if (other.gameObject.CompareTag("BoundRange"))
        {
            BoundCount = false;
        }
    }

    ////オブジェクト設置しているかを返すゲッター
    //public bool GetJumpCount()
    //{
    //    return jumpCount;
    //}
}