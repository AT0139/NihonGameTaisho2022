using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EffectLanding : MonoBehaviour
{
    public ParticleSystem StrongLanding;
    public ParticleSystem WeekLanding;

    public float minSpeed = 10.0f;
    private ParticleSystem strongLanding;
    private ParticleSystem weekLanding;
    private bool IsFire;

    float movementSpeed;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        strongLanding = Instantiate(StrongLanding, transform.position, Quaternion.identity);
        strongLanding.Stop();

        weekLanding = Instantiate(WeekLanding, transform.position, Quaternion.identity);
        weekLanding.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        movementSpeed = rigidbody2D.velocity.magnitude;
        

        if(movementSpeed >= minSpeed)
        {
            IsFire = true;
        }else
        {
            IsFire = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Ground Middle"  ||
            collision.transform.tag == "Ground"         ||
            collision.transform.tag == "Ground Soft"    ||
            collision.transform.tag == "Ground Hard"    ||
            collision.transform.tag == "Wall")
        {
            if (IsFire == true)
            {
                //Debug.Log(movementSpeed);
                strongLanding.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                strongLanding.Play();
            }
            else
            {
                weekLanding.transform.position = transform.position;
                weekLanding.Play();
            }
        }
    }
}

