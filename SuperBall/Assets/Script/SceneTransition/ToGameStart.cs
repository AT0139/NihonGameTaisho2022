using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGameStart : MonoBehaviour
{
    GameObject sceneControllerCam;    
    
    public float TransitionTime = 2.0f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sceneControllerCam = Camera.main.gameObject;
    
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            animator.SetBool("blMove", true);
            Invoke("ChangeStageSelectScene", TransitionTime);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Invoke("AnimatorSetBoolFalse", 1.0f);
    }

    void AnimatorSetBoolFalse()
    {
        animator.SetBool("blMove", false);
    }


    void ChangeStageSelectScene()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene");
    }
}
