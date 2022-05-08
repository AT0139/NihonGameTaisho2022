using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    public AudioClip jumpSound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground Middle")
        {
            audioSource.PlayOneShot(jumpSound);
        }
        if (collision.gameObject.tag == "Ground Hard")
        {
            audioSource.PlayOneShot(jumpSound);
        }
        if (collision.gameObject.tag == "Wall")
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

}
