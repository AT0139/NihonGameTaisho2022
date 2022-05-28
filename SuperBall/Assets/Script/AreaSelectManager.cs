using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class AreaSelectManager : MonoBehaviour
{
    [SerializeField] int selectArea;

    Camera sceneCamera;
    GameObject selectObject;
    int objectNo = 0;



    private void Start()
    {
        sceneCamera = Camera.main;
        EventSystem.current.SetSelectedGameObject(GameObject.Find("ButtonArea01"));
    }

    private void Update()
    {
        selectObject = EventSystem.current.currentSelectedGameObject;
        Vector2 pos = selectObject.transform.position;
        sceneCamera.transform.position = new Vector3(pos.x, pos.y, -10);

        //-------------パッド-------------------
        //決定ボタン
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {

            }
        }

        //-------------キーボード---------------
        //エンター
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (selectObject.name == "ButtonToTitle")
                sceneCamera.gameObject.GetComponent<SceneController>().sceneChange("TitleScene_new");
            else
            {
                Debug.Log(selectObject.name);
                sceneCamera.gameObject.GetComponent<SceneController>().sceneChange("StageSelectScene" + selectObject.name.Remove(0, 10));
            }
        }
        //右矢印
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            SwitchButton(false);
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            SwitchButton(true);
        }
    }
    void SwitchButton(bool isLeft)
    {
        //右
        if(!isLeft)
        {
            objectNo++;
            if (objectNo >= 5)
                objectNo = 5;
            GameObject obj = GameObject.Find("ButtonArea0" + objectNo);
            Debug.Log(obj);
            EventSystem.current.SetSelectedGameObject(obj);
        }
        else
        {
            objectNo--;
            if (objectNo <= 1)
                objectNo = 1;
            GameObject obj = GameObject.Find("ButtonArea0" + objectNo);
            Debug.Log(obj);
            EventSystem.current.SetSelectedGameObject(obj);
        }
    }
}

