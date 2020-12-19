using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    public AudioClip soundToPlay;
    public float volume;
    AudioSource audio;

    // public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // CheckPlayerPosition();
    }

    /* void CheckPlayerPosition()
    {
        if (GetComponent<SpriteRenderer>().enabled == true && Vector3.Distance(player.transform.position, transform.position) <= 3f)
        {
            if(gameObject.CompareTag("Diamond"))
            {
                player.GetComponent<PlayerMovement>().score += 1;
                audio.PlayOneShot(soundToPlay, volume);
            }
            else if(gameObject.CompareTag("HealthBoost"))
            {
                player.GetComponent<PlayerMovement>().health += 10;
                audio.PlayOneShot(soundToPlay, volume);
            }

            Destroy(gameObject);
        }
    } */

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
        if(player != null && gameObject.GetComponent<SpriteRenderer>().enabled == true)
        {
            player.score += 1;
            audio.PlayOneShot(soundToPlay, volume);
            Destroy(gameObject);
        }
    }
}