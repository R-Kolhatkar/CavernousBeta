﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public AudioClip soundToPlay;
    public float volume;
    AudioSource audio;
    // public bool alreadyPlayed = false;

    float ActiveTimer;
    bool active = false;
    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        ActiveTimer = 10f;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            ActiveTimer -= Time.deltaTime;
            if(ActiveTimer <= 0)
            {
                ActiveTimer = 10f;
                active = true;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            ActiveTimer -= Time.deltaTime;
            if (ActiveTimer <= 0)
            {
                ActiveTimer = 10f;
                active = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
        if (player != null && gameObject.GetComponent<SpriteRenderer>().enabled == true)
        {
            player.health -= 10;
            audio.PlayOneShot(soundToPlay, volume);
        }
    }
}
