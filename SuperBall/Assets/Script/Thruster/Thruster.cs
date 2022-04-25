using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// DX
/*==============================================================================
  Thruster
														 Author :君島朝日
														 Date   :2022/03/10
--------------------------------------------------------------------------------
スラスターの処理をするスクリプト
==============================================================================*/

public class Thruster : MonoBehaviour
{
    [SerializeField] bool isButton = false;

    // 横方向の速度
    public float X_Speed;

    // 縦方向の速度
    public float Y_Speed;

    //trueにすると速度を0にしてからスラスターを使用する
    public bool AirDushMode;

    public float X_SpeedAirDush;
    public float Y_SpeedAirDush;

    // スラスターを使える状態かのチェック
    private bool SwitchThrusterCheck;

    // レンジに入ってどれぐらいの時間で使えるようになるか 0.5秒の場合は0.5と入力
    public float ThrusterCooltime;

    // 入力状態のときにどれぐらいの時間で再判定するか 0.5秒の場合は0.5と入力
    public float ThrusterAgain;

    // ログ出力に使用　必要なければ消してよし
    private bool AgainCheck;

    private Rigidbody2D rb;

    private Vector2 move;
    private Vector2 Lstick;

    PlayerActionInput input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SwitchThrusterCheck = false;

        AgainCheck = false; 

        //InputSystemを有効化
        input = new PlayerActionInput();

        input.Enable();
        input.Player.ThrusterButton.performed += context => ButtonThruster();
    }

    void Update()
    {
        if (SwitchThrusterCheck)
            UseThruster();

        move = input.Player.Thruster.ReadValue<Vector2>();
        
    }

    //スラスターを使って移動を行う関数
    void UseThruster()
    {
        if (!isButton)
        {
            // コントローラーの傾け具合で方向が変えられる
            if (move.x != 0 || move.y != 0)
            {
                // AirDushMode無効
                if (!AirDushMode)
                {
                    //左右の移動
                    rb.AddForce(transform.right * X_Speed * move.x, ForceMode2D.Impulse);
                    //上下の移動
                    rb.AddForce(transform.up * Y_Speed * move.y, ForceMode2D.Impulse);
                    SwitchThrusterCheck = false;
                }
                // AirDushMode有効
                else
                {
                    rb.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

                    //左右の移動
                    rb.AddForce(transform.right * X_SpeedAirDush * move.x, ForceMode2D.Impulse);
                    //上下の移動
                    rb.AddForce(transform.up * Y_SpeedAirDush * move.y, ForceMode2D.Impulse);
                    SwitchThrusterCheck = false;
                }
            }
        }
    }

    // 侵入判定
    void OnTriggerEnter2D(Collider2D other)
    {
        // スラスターレンジに入ったら
        if (other.gameObject.CompareTag("Thruster Range"))
        {
            if(!SwitchThrusterCheck)
            Invoke("SwitchThruster", ThrusterCooltime);
        }
    }


    void SwitchThruster()
    {
        if(move.x == 0 && move.y == 0)
        {
            // ログ出力が変わるだけなので後で消してもよい
            if(AgainCheck)
            {
                Debug.Log("再判定成功、スラスター使用可能");
                AgainCheck = false;
            }
            else
            {
                Debug.Log(ThrusterCooltime + "秒後にスラスター使用可能になった");
            }

            SwitchThrusterCheck = true;
        }
        else
        {
            // 実質再帰関数？
            Invoke("SwitchThruster", ThrusterAgain);
            Debug.Log(ThrusterAgain + "秒後にスラスター再判定");
            AgainCheck = true;
        }
        
    }

    public void ButtonThruster()
    {
        if (SwitchThrusterCheck)
        {
            Lstick = input.Player.Move.ReadValue<Vector2>();
            // AirDushMode無効
            if (!AirDushMode)
            {
                //左右の移動
                rb.AddForce(transform.right * X_Speed * Lstick.x, ForceMode2D.Impulse);
                //上下の移動
                rb.AddForce(transform.up * Y_Speed * Lstick.y, ForceMode2D.Impulse);
                SwitchThrusterCheck = false;
            }
            // AirDushMode有効
            else
            {
                rb.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

                //左右の移動
                rb.AddForce(transform.right * X_SpeedAirDush * Lstick.x, ForceMode2D.Impulse);
                //上下の移動
                rb.AddForce(transform.up * Y_SpeedAirDush * Lstick.y, ForceMode2D.Impulse);
                SwitchThrusterCheck = false;
            }
        }
    }
}