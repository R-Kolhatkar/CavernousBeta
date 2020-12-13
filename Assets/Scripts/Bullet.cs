﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public float lifeTime;
    Rigidbody2D rb;
    public GameObject destroyEffect;
    GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyMovement enemy = hitInfo.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Instantiate(impactEffect, transform.position, transform.rotation);

        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(effect);
        Destroy(gameObject);
    }
}
