using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    float ActiveTimer;
    bool active = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ActiveTimer = 20f;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            ActiveTimer -= 1;
            if(ActiveTimer <= 0)
            {
                ActiveTimer = 20f;
                active = true;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            ActiveTimer -= Time.deltaTime;
            if (ActiveTimer <= 0)
            {
                ActiveTimer = 20f;
                active = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.health -= 10;
        }
    }
}
