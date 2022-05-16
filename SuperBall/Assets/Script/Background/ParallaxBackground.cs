using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    bool isInitialized = false;

    [Header("背景画像 (0が最奥、順に手前)")]
    [SerializeField]
    Sprite[] backgroundSprites;

    [Header("背景画像のオフセット (ズラす値)(左右スクロール対応の場合は1画像分、左にズラす)")]
    [SerializeField]
    Vector2[] backgroundOffsets;

    [Header("背景画像のサイズ")]
    [SerializeField]
    Vector2[] backgroundSpriteSizes;

    [Header("背景画像のXスクロール率 (奥(0)の物程小さめに指定)")]
    [SerializeField]
    float[] scrollRatesX;

    [Header("背景画像のYスクロール率 (奥(0)の物程小さめに指定)")]
    [SerializeField]
    float[] scrollRatesY;

    //3Dオブジェクト(キャラクター等)より奥になるように調整。カメラの位置や3Dオブジェクトのサイズによるが30～設定すれば良い。
    [Header("カメラからUIへの距離 (カメラの位置や3Dオブジェクトのサイズによるが30～指定)")]
    [Range(30.0f, 100.0f)]
    [SerializeField]
    float planeDistance = 30.0f;

    [Header("背景画像を左右に何個配置するか (右スクロールのみなら2、左右スクロール対応なら3)")]
    [Range(2, 3)]
    [SerializeField]
    int imageMax = 2;

    [Header("スクロール時間")]
    [Range(0.1f, 3.0f)]
    [SerializeField]
    float scrollDuration = 1.0f;


    [Header("スクロール速度の上限 (多分deltaTimeが掛かるので大きめに指定)")]
    [Range(500.0f, 10000.0f)]
    [SerializeField]
    float scrollSpeedMax = 1000.0f;



    //各背景画像のRectTransform。
    [HideInInspector]
    [SerializeField]
    RectTransform[] backgroundsRt;

    //背景画像数。
    [HideInInspector]
    [SerializeField]
    int backgroundMax;

    //各背景画像がスクロールした量。
    [HideInInspector]
    [SerializeField]
    Vector2[] backgroundScrollValues;

    //RectMask2Dを有効にした状態で実行すると、画面外に設置した画像がスクロールしても非表示にされるので、実行時に有効化している。
    [HideInInspector]
    [SerializeField]
    RectMask2D parallaxBackgroundRectMask2D;

    //スクロール経過時間。
    float scrollElapsedTime;

    //スクロール加速度。SmoothDampに必要。
    [HideInInspector]
    [SerializeField]
    Vector2[] scrollVelocities;

    //コルーチンの管理に使用。
    Coroutine scroll;

    //前にスクロールが呼ばれた時のプレイヤーの位置。
    Vector3 previousPlayerPosition = Vector3.zero;

    [SerializeField]
    GameObject  groundLevelUpperLimit;
    [SerializeField]
    GameObject  groundLevelLowerLimit;
    [SerializeField]
    GameObject  player;

    float       YDistBetweenGroundLevels;
    Vector3     playerPosition;
    float       DistBetweenPlayerAndGround;
    float       UPPERLIMIT;
    float       LOWERLIMIT;
    [SerializeField]
    float OFFSET;

    //一時的に使用。
    Canvas parallaxBackgroundCanvas;
    GameObject parallaxBackgroundGo;
    RectTransform parallaxBackgroundRt;

    GameObject tempBackgroundGo;
    RectTransform tempBackgroundRt;
    Image tempBackgroundImg;
    Vector2 tempBackgroundPosition;
    Vector2 tempBackgroundsPosition;

    void Awake()
    {
        if (!isInitialized)
            CreateParallaxBackground();

        parallaxBackgroundRectMask2D.enabled = true;

        playerPosition = player.transform.position;

        YDistBetweenGroundLevels    = Mathf.Abs(groundLevelUpperLimit.transform.position.y - groundLevelLowerLimit.transform.position.y);
        UPPERLIMIT                  = groundLevelUpperLimit.transform.position.y;
        LOWERLIMIT                  = groundLevelLowerLimit.transform.position.y;

        
    }


    //背景画像をスクロールしたい場合にコレを呼ぶ。引数にはプレイヤーの位置を渡す(位置差でなく)。
    public void StartScroll(Vector3 playerPosition)
    {
        //右スクロールのみに対応時、プレイヤーが左に進んだ場合は無視する。
        if (imageMax == 2 && playerPosition.x - previousPlayerPosition.x < 0)
            return;
        //1画像分進んだ時、スクロールが繋がるように上手く戻している。
        for (int i = 0; i < backgroundMax; i++)
        {
            float hosei;
            var currentPlayerPosY = player.transform.position.y;
            var StageLowestPosY = groundLevelLowerLimit.transform.position.y;
            var StageHighestPosY = groundLevelUpperLimit.transform.position.y;

            hosei = currentPlayerPosY <= 0 && StageLowestPosY <= 0 ?                                                  // プレイヤーの位置が0以下 && ステージの下限が0以下？
                StageLowestPosY - (Mathf.Abs(StageLowestPosY) - Mathf.Abs(StageLowestPosY) * (currentPlayerPosY / StageLowestPosY)) :   // はい
                0;                                                                                                                      // いいえ

            var stageSizeY = Mathf.Abs(StageLowestPosY - StageHighestPosY);
            var stageCenterY = Mathf.Abs(StageLowestPosY - StageHighestPosY) / 2f /*+ hosei*/;
            // 地表から
            var distFromsurface = Mathf.Abs(StageLowestPosY - currentPlayerPosY);
            var proportion = distFromsurface / stageSizeY;
            // 地表と天井間の中心
            var distFromCenter = StageLowestPosY + stageCenterY - currentPlayerPosY /*+ hosei*/;
            var proportionC = distFromCenter / stageCenterY;
            var moveAmountsC = (backgroundSpriteSizes[i].y - 1080f) / 2f /*+ hosei*/;    //  制作中の現在、Screen.height の値が想定した1080ではないため直接1080を渡している。
            var moveAmounts = (backgroundSpriteSizes[i].y - 1080f) / 2f;    //  制作中の現在、Screen.height の値が想定した1080ではないため直接1080を渡している。

            //Debug.Log(proportion);
            //Debug.Log(proportionC);
            backgroundScrollValues[i].x -= (playerPosition.x - previousPlayerPosition.x) * scrollRatesX[i];
            //backgroundScrollValues[i].y -= (playerPosition.y - previousPlayerPosition.y) * scrollRatesY[i];
            if(i == 0) {
                backgroundScrollValues[i].y = moveAmountsC * proportionC - OFFSET;
                backgroundsRt[i].anchoredPosition = backgroundScrollValues[i];
            }
            else {
                backgroundScrollValues[i].y = moveAmounts + -moveAmounts * proportion * scrollRatesY[i] - OFFSET;
                backgroundsRt[i].anchoredPosition = backgroundScrollValues[i];
            }

            if (backgroundSpriteSizes[i].x < backgroundsRt[i].anchoredPosition.x)
            {
                backgroundScrollValues[i].x -= backgroundSpriteSizes[i].x;
                tempBackgroundsPosition.Set(backgroundSpriteSizes[i].x, tempBackgroundsPosition.y);
                backgroundsRt[i].anchoredPosition -= tempBackgroundsPosition;
                //backgroundsRt[i].anchoredPosition = new Vector2(backgroundsRt[i].anchoredPosition.x, backgroundScrollValues[i].y);
            }
            else if (backgroundsRt[i].anchoredPosition.x < -backgroundSpriteSizes[i].x)
            {
                backgroundScrollValues[i].x += backgroundSpriteSizes[i].x;
                tempBackgroundsPosition.Set(backgroundSpriteSizes[i].x, tempBackgroundsPosition.y);
                backgroundsRt[i].anchoredPosition += tempBackgroundsPosition;
                //backgroundsRt[i].anchoredPosition = new Vector2(backgroundsRt[i].anchoredPosition.x, backgroundScrollValues[i].y);
            }
            //backgroundsRt[i].anchoredPosition = new Vector2(backgroundsRt[i].anchoredPosition.x, backgroundScrollValues[i].y);
        }


        Debug.Log("StartScroll");
        Debug.Log(tempBackgroundsPosition);
        Debug.Log(backgroundsRt[0].anchoredPosition);
        Debug.Log(backgroundsRt[1].anchoredPosition);
        Debug.Log(playerPosition.y);

        //多重実行防止。
        if (scroll != null)
        {
            StopCoroutine(scroll);
        }

        scroll = StartCoroutine(Scroll());

        
        previousPlayerPosition = playerPosition;
    }


    IEnumerator Scroll()
    {
        scrollElapsedTime = 0;
        while (true)
        {
            scrollElapsedTime += Time.deltaTime;
            for (int i = 0; i < backgroundMax; i++)
            {
                tempBackgroundsPosition.Set(backgroundScrollValues[i].x, backgroundScrollValues[i].y/*backgroundOffsets[i].y*/);
                backgroundsRt[i].anchoredPosition = 
                    new Vector2 (Vector2.SmoothDamp(
                        backgroundsRt[i].anchoredPosition, // Vector2 current
                        tempBackgroundsPosition,           // Vector2 target
                        ref scrollVelocities[i],           // Vector2 currentVelocity
                        scrollDuration,                    // smoothTime
                        scrollSpeedMax                     // maxSpeed = Mathf>Infinity
                    ).x, backgroundScrollValues[i].y);
                backgroundsRt[i].anchoredPosition = new Vector2(backgroundsRt[i].anchoredPosition.x, backgroundScrollValues[i].y - OFFSET);
            }

            //Debug.Log("Scroll");
            //Debug.Log(tempBackgroundPosition);
            //Debug.Log(tempBackgroundsPosition);
            //Debug.Log(backgroundsRt[0].anchoredPosition);
            //Debug.Log(backgroundsRt[1].anchoredPosition);
            if (scrollDuration <= scrollElapsedTime)
            {
                //SmoothDampはVelocityの値を参考にして現在の速度を出す為、初期化しておかないと次回実行時に動きが残る。
                for (int i = 0; i < backgroundMax; i++)
                {
                    scrollVelocities[i] = Vector2.zero;
                }

                scroll = null;
                yield break;
            }

            yield return null;
        }
    }


    //ステージクリア等で画像位置を強制的にリセットする時用。
    public void Reset()
    {
        for (int i = 0; i < backgroundMax; i++)
        {
            backgroundScrollValues[i] = Vector2.zero;

            tempBackgroundsPosition.Set(backgroundScrollValues[i].x, backgroundOffsets[i].y);
            backgroundsRt[i].anchoredPosition = tempBackgroundsPosition;
        }

        for (int i = 0; i < backgroundMax; i++)
        {
            scrollVelocities[i] = Vector2.zero;
        }

        previousPlayerPosition = Vector3.zero;

        if (scroll != null)
        {
            StopCoroutine(scroll);
            scroll = null;
        }
    }


    //各種コンポーネントをアタッチし、背景画像等を生成。
    public void CreateParallaxBackground()
    {
        if (backgroundSprites == null || backgroundSprites.Length == 0)
            return;

        backgroundMax = backgroundSprites.Length;


        parallaxBackgroundCanvas = GetComponent<Canvas>();

        if (parallaxBackgroundCanvas == null)
            return;


        backgroundsRt = new RectTransform[backgroundMax];
        scrollVelocities = new Vector2[backgroundMax];
        backgroundScrollValues = new Vector2[backgroundMax];


        parallaxBackgroundCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        parallaxBackgroundCanvas.worldCamera = Camera.main;
        parallaxBackgroundCanvas.planeDistance = planeDistance;

        //ボタンを設置しないので、このCanvasへのタッチ判定を無効化しておく(インスペクターから削除しても良い)。
        GetComponent<GraphicRaycaster>().enabled = false;


        parallaxBackgroundGo = new GameObject("ParallaxBackground");
        parallaxBackgroundRt = parallaxBackgroundGo.AddComponent<RectTransform>();
        parallaxBackgroundRectMask2D = parallaxBackgroundGo.AddComponent<RectMask2D>();
        parallaxBackgroundRectMask2D.enabled = false;
        parallaxBackgroundRt.SetParent(transform); // transform = canvas

        parallaxBackgroundRt.localScale = Vector3.one;
        parallaxBackgroundRt.localPosition = Vector3.zero;
        parallaxBackgroundRt.sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;


        for (int i = 0; i < backgroundMax; i++)
        {
            backgroundsRt[i] = new GameObject(System.String.Format("Backgrounds{0}", i + 1)).AddComponent<RectTransform>();
            backgroundsRt[i].SetParent(parallaxBackgroundRt);

            backgroundsRt[i].localScale = Vector3.one;
            backgroundsRt[i].localPosition = Vector3.zero;

            tempBackgroundPosition.Set(0, backgroundOffsets[i].y);
            backgroundsRt[i].anchoredPosition = tempBackgroundPosition;


            for (int j = 0; j < imageMax; j++)
            {
                tempBackgroundGo = new GameObject(System.String.Format("Background{0}", i + 1));
                tempBackgroundRt = tempBackgroundGo.AddComponent<RectTransform>();
                tempBackgroundImg = tempBackgroundGo.AddComponent<Image>();
                tempBackgroundImg.sprite = backgroundSprites[i];
                tempBackgroundImg.raycastTarget = false;

                tempBackgroundRt.SetParent(backgroundsRt[i]);
                tempBackgroundRt.localScale = Vector3.one;
                tempBackgroundRt.localPosition = Vector3.zero;


                tempBackgroundRt.sizeDelta = backgroundSpriteSizes[i];
                
                float hosei;
                var currentPlayerPosY   = player.transform.position.y;
                var StageLowestPosY     = groundLevelLowerLimit.transform.position.y;
                var StageHighestPosY    = groundLevelUpperLimit.transform.position.y;
                    
                hosei                   = currentPlayerPosY <= 0 && StageLowestPosY <= 0 ?                                                  // プレイヤーの位置が0以下 && ステージの下限が0以下？
                    StageLowestPosY - (Mathf.Abs(StageLowestPosY) - Mathf.Abs(StageLowestPosY) * (currentPlayerPosY / StageLowestPosY)) :   // はい
                    0;                                                                                                                      // いいえ

                var stageSizeY          = Mathf.Abs(StageLowestPosY - StageHighestPosY);
                var stageCenterY        = Mathf.Abs(StageLowestPosY - StageHighestPosY) / 2f /*+ hosei*/;
                // 地表から
                var distFromsurface     = Mathf.Abs(StageLowestPosY - currentPlayerPosY);
                var proportion          = distFromsurface / stageSizeY;
                // 地表と天井間の中心
                var distFromCenter      = StageLowestPosY + stageCenterY - currentPlayerPosY /*+ hosei*/;
                var proportionC         = distFromCenter / stageCenterY;
                var moveAmountsC         = (backgroundSpriteSizes[i].y - 1080f) / 2f /*+ hosei*/;    //  制作中の現在、Screen.height の値が想定した1080ではないため直接1080を渡している。
                var moveAmounts         = (backgroundSpriteSizes[i].y - 1080f) / 2f;    //  制作中の現在、Screen.height の値が想定した1080ではないため直接1080を渡している。
                if (i == 0) {
                    tempBackgroundPosition.Set(
                        backgroundOffsets[i].x + backgroundSpriteSizes[i].x * j,
                        moveAmountsC * proportionC
                    );
                    Debug.Log("後");
                    Debug.Log(proportionC);
                }
                else {
                    Debug.Log("前");
                    Debug.Log(proportion);
                    Debug.Log(moveAmounts);
                    tempBackgroundPosition.Set(
                        backgroundOffsets[i].x + backgroundSpriteSizes[i].x * j,
                        moveAmounts + -moveAmounts * proportion * scrollRatesY[i]
                    );
                }

                //if (tempBackgroundPosition.y + backgroundSpriteSizes[i].y * 0.5f < Screen.height)
                //{
                //    Debug.Log("screentopより小さい");
                //    tempBackgroundPosition.Set(
                //        backgroundOffsets[i].x + backgroundSpriteSizes[i].x * j,
                //        Screen.height - backgroundSpriteSizes[i].y * 0.5f
                //    );
                //    backgroundsRt[i].anchoredPosition.Set(backgroundOffsets[i].x + backgroundSpriteSizes[i].x * j, Screen.height - backgroundSpriteSizes[i].y * 0.5f);
                //}
                //if (tempBackgroundPosition.y - backgroundSpriteSizes[i].y * 0.5f > 0)
                //{
                //    Debug.Log("screenbottomより大きい");
                //    tempBackgroundPosition.Set(
                //        backgroundOffsets[i].x + backgroundSpriteSizes[i].x * j, 
                //        0 + backgroundSpriteSizes[i].y * 0.5f
                //    );
                //    backgroundsRt[i].anchoredPosition.Set(backgroundOffsets[i].x + backgroundSpriteSizes[i].x * j, 0 + backgroundSpriteSizes[i].y * 0.5f);
                //}
                tempBackgroundRt.anchoredPosition = tempBackgroundPosition;
            }
        }

        isInitialized = true;
    }
    /*
     * 
     * float hosei;
                var currentPlayerPosY   = player.transform.position.y;
                var StageLowestPosY     = groundLevelLowerLimit.transform.position.y;
                var StageHighestPosY    = groundLevelUpperLimit.transform.position.y;
                    
                hosei                   = currentPlayerPosY <= 0 && StageLowestPosY <= 0 ?                                                  // プレイヤーの位置が0以下 && ステージの下限が0以下？
                    StageLowestPosY - (Mathf.Abs(StageLowestPosY) - Mathf.Abs(StageLowestPosY) * (currentPlayerPosY / StageLowestPosY)) :   // はい
                    0;                                                                                                                      // いいえ
                
                var stageSizeY          = Mathf.Abs(StageLowestPosY - StageHighestPosY);
                var stageCenterY        = Mathf.Abs(StageLowestPosY - StageHighestPosY) / 2f + hosei;
                // 地表から
                var distFromsurface     = Mathf.Abs(StageLowestPosY - currentPlayerPosY);
                var proportion          = distFromsurface / stageSizeY;
                // 地表と天井間の中心
                var distFromCenter      = stageCenterY  - currentPlayerPosY + hosei;
                var proportionC         = distFromCenter / stageCenterY;
                var moveAmountsC         = (backgroundSpriteSizes[i].y - 1080f) / 2f + hosei;    //  制作中の現在、Screen.height の値が想定した1080ではないため直接1080を渡している。
                var moveAmounts         = (backgroundSpriteSizes[i].y - 1080f) / 2f;    //  制作中の現在、Screen.height の値が想定した1080ではないため直接1080を渡している。
     */
}
