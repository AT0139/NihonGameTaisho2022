using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameSelectStage : MonoBehaviour
{
    GameObject sceneControllerCam;

    // ステージ選択ではマウスで選択

    public void ToTitleScene()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("TitleScene_new");
    }
    public void ToAreaSelectScene()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("AreaSelectScene");
    }

    public void ToGameScene_11()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene11");
    }

    public void ToGameScene_12()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene12");
    }

    public void ToGameScene_13()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene13");
    }

    public void ToGameScene_14()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene14");
    }

    public void ToGameScene_15()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene15");
    }

    public void ToGameScene_21()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene21");
    }

    public void ToGameScene_22()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene22");
    }

    public void ToGameScene_23()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene23");
    }

    public void ToGameScene_24()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene24");
    }

    public void ToGameScene_25()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene25");
    }

    public void ToGameScene_31()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene31");
    }

    public void ToGameScene_32()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene32");
    }

    public void ToGameScene_33()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene33");
    }

    public void ToGameScene_34()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene34");
    }

    public void ToGameScene_35()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene35");
    }

    public void ToGameScene_41()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene41");
    }

    public void ToGameScene_42()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene42");
    }

    public void ToGameScene_43()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene43");
    }

    public void ToGameScene_44()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene44");
    }

    public void ToGameScene_45()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene45");
    }

    public void ToGameScene_51()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene51");
    }

    public void ToGameScene_52()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene52");
    }

    public void ToGameScene_53()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene53");
    }

    public void ToGameScene_54()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene54");
    }

    public void ToGameScene_55()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("GameScene55");
    }

    public void ToStageSelectScene01()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene01");
    }
    public void ToStageSelectScene02()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene02");
    }
    public void ToStageSelectScene03()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene03");
    }
    public void ToStageSelectScene04()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene04");
    }
    public void ToStageSelectScene05()
    {
        sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene05");
    }


    // Start is called before the first frame update
    void Start()
    {
        sceneControllerCam = Camera.main.gameObject;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
