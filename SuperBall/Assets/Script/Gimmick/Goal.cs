using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Goal : MonoBehaviour
{
    GameObject sceneControllerCam;
    [SerializeField] GameObject playerGoal;
    [SerializeField] float animationTime = 1.0f;

    [SerializeField] GameObject coin;
    [SerializeField] float awakeTime = 1.0f;

    bool IsEnter2D = false;
    bool IsCoin = false;
    bool IsLeave = false;
    private GameObject playerGoalInstance;
    private float coinTime = 1.0f;
    private Rigidbody2D ridig2DPlayerGoal;
    [SerializeField] float oneCoinTime = 0.1f;
    private Animator playerGoalAnimator;
    private GameObject CMvcam1;

    // Start is called before the first frame update
    void Start()
    {
        sceneControllerCam = Camera.main.gameObject;
        ScoreManager.scoreManagerInstance.AddScore(10);
        CMvcam1 = GameObject.Find("CM vcam1");
    }

    void IsCoinTrue()
    {
        IsEnter2D = false;
        IsCoin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEnter2D)
        {
            IsCoinTrue();            
        }

        if (IsCoin)
        {
            playerGoalInstance.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 50, 0));
            Invoke("startCoroutin", 1.0f);
            IsCoin = false;
        }
        

        if (IsLeave)
        {
            //  これはコイン処理が終わってからにしたい。
            //  「去る」アニメーションフラグ設定
            playerGoalAnimator.SetBool("IsLeave", true);

            
            coinTime *= ScoreManager.scoreManagerInstance.GetCoinNum();
            
            

            //  アニメーション遷移時間＋コイン処理時間
            float totalTime = animationTime + coinTime;

            //  シーン遷移・フラグ初期化
            Invoke("SetIsLeaveFalse", totalTime);
        }
    }
    void startCoroutin()
    {
        StartCoroutine("CoinThrowCol");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "Player")
        {            

            IsEnter2D = true;

            //  プレイヤーをそのまま使うのは変更するのがだるいので
            //  新しいゴール用のプレイヤーを使う

            collision.gameObject.SetActive(false);

            playerGoalInstance = Instantiate(playerGoal, collision.transform.position, Quaternion.identity);
            ridig2DPlayerGoal = playerGoalInstance.GetComponent<Rigidbody2D>();
            Camera.main.gameObject.GetComponent<Animator>().SetBool("IsGoal", true);
            playerGoalAnimator = playerGoalInstance.GetComponent<Animator>();
            playerGoalAnimator.SetBool("IsEnter", true);

            CMvcam1.GetComponent<CinemachineVirtualCamera>().Follow = playerGoalInstance.transform;
            
        }
    }

    private IEnumerator CoinThrowCol()
    {
        int coinNum = ScoreManager.scoreManagerInstance.GetCoinNum();
        for (int i = 0; i < coinNum; i++)
        {
            GameObject coinInstance = Instantiate(coin, playerGoalInstance.transform.position, Quaternion.identity);
            ScoreManager.scoreManagerInstance.SubstractScore(1);
            Debug.Log("i:"+i);
            yield return new WaitForSeconds(oneCoinTime);
        }

        IsLeave = true;               
    }


    private void SetIsLeaveFalse()
    {
        playerGoalAnimator.SetBool("IsLeave", false);
        playerGoalAnimator.SetBool("IsGoal", false);
        sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            IsEnter2D = false;
        }
    }
}
