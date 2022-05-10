using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBGManager : MonoBehaviour
{
    GameObject parallaxBG;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(parallaxBG)
            parallaxBG.GetComponent<ParallaxBackground>().StartScroll(transform.position);
    }
}
