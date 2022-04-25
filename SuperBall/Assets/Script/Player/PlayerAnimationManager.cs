using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    enum COLLISIONDILECTION{
        UP,
        DOWN,
        RIGHT,
        LEFT = RIGHT
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovingDirectionHor();
        //Debug.Log("magni");
        //Debug.Log(GetComponent<Rigidbody2D>().velocity.magnitude);
        GetComponent<Animator>().SetFloat("Speed", GetComponent<Rigidbody2D>().velocity.magnitude);
        if(Input.GetKey(KeyCode.N))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D point in collision.contacts)
        {
            Vector3 relativePoint = transform.InverseTransformPoint(point.point);
            if(GetComponent<Rigidbody2D>().velocity.magnitude > 7)
            {
                if(relativePoint.x > 0.2)
                {
                    //Debug.Log("Right");
                    GetComponent<Animator>().SetInteger("trans", (int)COLLISIONDILECTION.RIGHT);
                    GetComponent<Animator>().Play("Bounce_Right_Player", 0, 0);
                }
                else if(relativePoint.x < -0.2)
                {
                    //Debug.Log("Left");
                    GetComponent<Animator>().SetInteger("trans", (int)COLLISIONDILECTION.LEFT);
                    GetComponent<Animator>().Play("Bounce_Right_Player", 0, 0);
                }

                if(relativePoint.y > 0.2)
                {
                    //Debug.Log("Up");
                    GetComponent<Animator>().SetInteger("trans", (int)COLLISIONDILECTION.UP);
                    GetComponent<Animator>().Play("Bounce_Up_Player", 0, 0);
                }
                else if(relativePoint.y < -0.2)
                {
                    //Debug.Log("Down");
                    GetComponent<Animator>().SetInteger("trans", (int)COLLISIONDILECTION.DOWN);
                    GetComponent<Animator>().Play("Bounce_Down_Player", 0, 0);
                }
            }
            else
            {
                GetComponent<Animator>().SetInteger("trans", -1);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GetComponent<Animator>().SetBool("IsGround", true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GetComponent<Animator>().SetBool("IsGround", false);
    }

    private void MovingDirectionHor()
    {
        //右向きか左向きか
        if(GetComponent<Rigidbody2D>().velocity.x > 0.1)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if(GetComponent<Rigidbody2D>().velocity.x < -0.1)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
