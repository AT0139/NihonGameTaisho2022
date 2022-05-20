using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    public Animator animator;

    [SerializeField]
    static float VEL = 5f;
    enum COLLISIONDILECTION{
        UP,
        DOWN,
        RIGHT,
        LEFT = RIGHT
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovingDirectionHor();
        //Debug.Log("magni");
        //Debug.Log(GetComponent<Rigidbody2D>().velocity.magnitude);
        animator.SetFloat("Speed", rigidbody2D.velocity.magnitude);
        if(Input.GetKey(KeyCode.N))
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D point in collision.contacts)
        {
            Vector3 relativePoint = transform.InverseTransformPoint(point.point);
            if(rigidbody2D.velocity.magnitude > VEL)
            {
                if(relativePoint.x > 0.2)
                {
                    //Debug.Log("Right");
                    animator.SetInteger("trans", (int)COLLISIONDILECTION.RIGHT);
                    animator.Play("Bounce_Right_Player", 0, 0);
                }
                else if(relativePoint.x < -0.2)
                {
                    //Debug.Log("Left");
                    animator.SetInteger("trans", (int)COLLISIONDILECTION.LEFT);
                    animator.Play("Bounce_Right_Player", 0, 0);
                }

                if(relativePoint.y > 0.2)
                {
                    //Debug.Log("Up");
                    animator.SetInteger("trans", (int)COLLISIONDILECTION.UP);
                    animator.Play("Bounce_Up_Player", 0, 0);
                }
                else if(relativePoint.y < -0.2)
                {
                    //Debug.Log("Down");
                    animator.SetInteger("trans", (int)COLLISIONDILECTION.DOWN);
                    animator.Play("Bounce_Down_Player", 0, 0);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        foreach (ContactPoint2D point in collision.contacts)
        {
            Vector3 relativePoint = transform.InverseTransformPoint(point.point);
            if (relativePoint.y < -0.2)
            {
                animator.SetBool("IsGround", true);
                if (rigidbody2D.velocity.magnitude <= VEL)
                {
                    animator.SetInteger("trans", -1);
                    animator.Play("Idle_Player", 0, 0);
                }
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("IsGround", false);
    }
    public void AnimationJump()
    {
        if(animator.GetBool("IsGround"))
        {
            animator.SetInteger("trans", (int)COLLISIONDILECTION.DOWN);
            animator.Play("Bounce_Down_Player", 0, 0);
        }
    }

    private void MovingDirectionHor()
    {
        //右向きか左向きか
        if(rigidbody2D.velocity.x > 0.5)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if(rigidbody2D.velocity.x < -0.5)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
