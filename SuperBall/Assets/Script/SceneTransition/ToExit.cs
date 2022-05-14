using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToExit : MonoBehaviour
{
    GameObject sceneControllerCam;

    GameObject player;          

    public float TransitionTime = 4.0f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sceneControllerCam = Camera.main.gameObject;
        player = GameObject.Find("Player");           
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
            Invoke("Quit", TransitionTime);
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



    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
