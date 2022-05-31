using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMmanager : MonoBehaviour
{
    // 登録するBGM
    [SerializeField] AudioSource Stageselect;

    void Start()
    {
        int numMusicPlayers = GameObject.FindGameObjectsWithTag("BGM").Length;
        // シーン内に他のBGMオブジェクトがあったらtrue
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            Stageselect.Play();

            //シーンが切り替わった時に呼ばれるメソッドを登録
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }
    }

    
    //シーンが切り替わった時に呼ばれるメソッド　
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        // 切り替え後selectシーンじゃなかったら prevSceneはnullが入るそうな、どういうことなの…
        if (!nextScene.name.Contains("Select"))
        {
            // 破壊してステージBGMなどの邪魔をしないように
            if(this != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
