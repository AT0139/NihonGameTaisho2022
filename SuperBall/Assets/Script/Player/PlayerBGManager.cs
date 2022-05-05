using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBGManager : MonoBehaviour
{
    GameObject parallaxBG;
    // Start is called before the first frame update
    void Start()
    {
        if (parallaxBG = GameObject.FindGameObjectWithTag("Background_Parallax"))
            ; 
        else
            Debug.LogError("視差背景がインスペクタ上に見つからないよ");
    }

    // Update is called once per frame
    void Update()
    {
        if(parallaxBG)
            parallaxBG.GetComponent<ParallaxBackground>().StartScroll(transform.position);
    }
}
