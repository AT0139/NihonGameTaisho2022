using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameObject sceneControllerCam;
    [SerializeField] GameObject playerGoal;
    [SerializeField] float animationTime = 1.0f;

    bool IsEnter2D = false;
    private GameObject playerGoalInstance;

    // Start is called before the first frame update
    void Start()
    {
        sceneControllerCam = Camera.main.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsEnter2D)
        {
            float playerXVelocity = (Player.rb.velocity.x) * (0.2f);
            Player.rb.velocity = new Vector3(playerXVelocity, 0, 0);
            if (playerXVelocity < 5.0f)
            {
                IsEnter2D = false;

                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "Player")
        {
            //collision.GetComponent<PlayerActionInput>().Disable();
            /*Player.input.Disable();
            Thruster.input.Disable();*/
            collision.gameObject.SetActive(false);

            IsEnter2D = true;
            playerGoalInstance = Instantiate(playerGoal, collision.transform.position, Quaternion.identity);

            playerGoalInstance.GetComponent<Animator>().SetBool("IsLeave", true);
            Invoke("SetIsLeaveFalse", animationTime);
        }
    }

    private void SetIsLeaveFalse()
    {
        playerGoalInstance.GetComponent<Animator>().SetBool("IsLeave", false);
        sceneControllerCam.GetComponent<SceneController>().sceneChange("StageSelectScene");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            IsEnter2D = false;
        }
    }
}
