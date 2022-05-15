using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public AudioClip ItemSound;
    AudioSource audioSource;

    [SerializeField] ParticleSystem getParticle;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Itemに衝突");
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(ItemSound, transform.position);//効果音をならす
            Instantiate(getParticle, transform.position, Quaternion.identity);
        }
    }
}
