using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    [SerializeField] float stopTime;
    bool isHitStop;
    int  timeCount;
    int hitstopDelay = 0;

    void Update()
    {
        hitstopDelay--;
        if(isHitStop)
        {
            //時間計測
            timeCount++;
            if(timeCount >= stopTime)
            {
                SetTimeNormal();
            }
        }
    }

    public void SetHitStop(float timeScale)
    {
        if (hitstopDelay <= 0)
        {
            timeCount = 0;
            Time.timeScale = timeScale;
            isHitStop = true;
            Debug.Log("ヒットストップ");
        }
    }

    void SetTimeNormal()
    {
        Time.timeScale = 1.0f;
        hitstopDelay = 30;
        isHitStop = false;
    }
}
