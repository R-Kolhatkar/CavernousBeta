using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerPosition();
    }

    void CheckPlayerPosition()
    {
        if (GetComponent<SpriteRenderer>().enabled == true && Vector3.Distance(player.transform.position, transform.position) <= 1f)
        {
            if(gameObject.CompareTag("Diamond"))
            {
                player.GetComponent<PlayerMovement>().score += 1;
            }
            else if(gameObject.CompareTag("HealthBoost"))
            {
                player.GetComponent<PlayerMovement>().health += 10;
            }

            Destroy(gameObject);
        }
    }
}