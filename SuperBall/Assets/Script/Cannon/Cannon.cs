using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Start is called before the first frame update
    public enum CANNONMODE{
        RIGHT,
        LEFT,
    }

    public CANNONMODE   cannonMode;
    public float        second          = 3;
    public float        optionalAngle   = 270;
    public float        bulletSpeed     = 7;
    public GameObject   cannonBullet;
    private float       angle;
    private Vector3     dir;
    Animator            animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(WaitShoot(second));
        switch (cannonMode)
        {
            case CANNONMODE.LEFT:
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                angle = 90;
                dir = Vector3.left;
                break;
            case CANNONMODE.RIGHT:
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                angle = 270;
                dir = Vector3.right;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Shoot()
    {
        var clone = Instantiate(cannonBullet, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
    }

    private IEnumerator WaitShoot(float second)
    {
        for(; ; )
        {
            yield return new WaitForSeconds(second);
            Shoot();
            animator.SetTrigger("Shooting");
        }
    }

    private void EndAnimationTrigger()
    {
        animator.SetTrigger("Shooting");
    }
}
