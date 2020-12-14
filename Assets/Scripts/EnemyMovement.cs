using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public float normalSpeed = 3f;
    public float chaseSpeed = 4f;
    float speed;
    public Transform leftWayPoint;
    public Transform rightWayPoint;
    bool movingRight = true;
    Rigidbody2D rb;

    public float stoppingDistance;
    public float retreatDistance;

    public GameObject player;

    Animator anim;
    bool dead = false;

    float dazedTime;
    public float startDazedTime;

    public GameObject deathEffect;
    GameObject effect;

    public GameObject diamond;

    public int health;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        speed = normalSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        diamond.transform.position = transform.position;
        diamond.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dazedTime <= 0)
        {
            speed = 3f;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        Move();
    }

    void Move()
    {
        if(!dead)
        {
            if(Vector2.Distance(player.transform.position, transform.position) <= 10f && player.transform.position.x < transform.position.x)
            {
                movingRight = false;
                speed = chaseSpeed;
            }
            else if(Vector2.Distance(player.transform.position, transform.position) <= 10f && player.transform.position.x > transform.position.x)
            {
                movingRight = true;
                speed = chaseSpeed;
            }
            else if (transform.position.x > rightWayPoint.position.x)
            {
                movingRight = false;
                speed = normalSpeed;
            }
            else if (transform.position.x < leftWayPoint.position.x)
            {
                movingRight = true;
                speed = normalSpeed;
            }

            if (movingRight == true)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
        else
        {

        }
    }

    void MoveLeft()
    {
        transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
        transform.localScale = new Vector2(-4, 4);
        diamond.transform.position = transform.position;
    }

    void MoveRight()
    {
        transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        transform.localScale = new Vector2(4, 4);
        diamond.transform.position = transform.position;
    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;

        health -= damage;
        
        if(health <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(WalkAgain(2f));
            anim.SetBool("isWalking", false);
            Debug.Log("Damage");
        }
    }

    IEnumerator WalkAgain(float delay)
    {
        yield return new WaitForSeconds(delay);

        anim.SetBool("isWalking", true);
    }

    void Die()
    {
        player.GetComponent<PlayerMovement>().numAliens -= 1;

        dead = true;

        anim.SetTrigger("Dead");

        GetComponent<SpriteRenderer>().enabled = false;

        effect = Instantiate(deathEffect, transform.position, Quaternion.identity);

        diamond.GetComponent<SpriteRenderer>().enabled = true;

        Destroy(effect);

        Destroy(gameObject);
    }
}
