using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameSelectStage : MonoBehaviour
{
    GameObject sceneControllerCam;

    // ステージ選択ではマウスで選択

    public void ToGameScene_01()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene01");
    }

    public void ToGameScene_02()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene02");
    }

    public void ToGameScene_03()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene03");
    }

    public void ToGameScene_04()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene04");
    }

    public void ToGameScene_05()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene05");
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneControllerCam = Camera.main.gameObject;
    }



    // Update is called once per frame
    void Update()
    {
        // Enterキー
       /* if (Input.GetKeyDown(KeyCode.Return))
        {
            //SceneManager.LoadScene("StageSelectScene");
            sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene");
        }*/
    }
}
