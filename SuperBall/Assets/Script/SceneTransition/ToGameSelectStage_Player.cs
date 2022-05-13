using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGameSelectStage_Player : MonoBehaviour
{
    GameObject sceneControllerCam;
    // Start is called before the first frame update
    void Start()
    {
        sceneControllerCam = Camera.main.gameObject;
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

    void ChangeStageSelectScene()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "GameStart")
        {
            Invoke("ChangeStageSelectScene", 3);
        }

        if (collision.transform.name == "Exit")
        {

            Invoke("Quit", 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
