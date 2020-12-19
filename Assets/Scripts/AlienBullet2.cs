using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet2 : MonoBehaviour
{
    public float speed = 50f;
    public int damage = 1;
    public float lifeTime;
    Rigidbody2D rb;
    public GameObject destroyEffect;
    GameObject effect;

    AudioSource source;
    public AudioClip playerImpacted;
    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        Debug.Log("Alien Bullet shooting");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Alien Bullet Position:\t" + transform.position);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Debug.Log(hitInfo);
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.health -= 5;

            source.PlayOneShot(playerImpacted, volume);

            DestroyProjectile();
        }

        // Instantiate(impactEffect, transform.position, transform.rotation);
    }

    void DestroyProjectile()
    {
        effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);

        Destroy(effect);
        Destroy(gameObject);
    }
}
