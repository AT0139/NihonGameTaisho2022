using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSE : MonoBehaviour
{
    public AudioClip SandSound;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(SandSound);
        }
    }
}
