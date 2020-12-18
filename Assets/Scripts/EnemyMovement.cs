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

    bool attackMode = false;
    bool attackRight = false;

    public Transform shotPoint;

    float dazedTime;
    public float startDazedTime;

    float attackTimer = 1000;

    public GameObject projectileRight;
    public GameObject projectileLeft;

    public GameObject deathEffect;
    GameObject effect;

    public GameObject diamond;

    public int health;

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
            if(Vector2.Distance(player.transform.position, transform.position) <= 5f && player.transform.position.x < transform.position.x)
            {
                attackMode = true;
                attackRight = false;
                // movingRight = false;
                speed = chaseSpeed;
            }
            else if(Vector2.Distance(player.transform.position, transform.position) <= 5f && player.transform.position.x > transform.position.x)
            {
                attackMode = true;
                attackRight = true;
                // movingRight = true;
                speed = chaseSpeed;
            }
            else if (transform.position.x > rightWayPoint.position.x)
            {
                attackMode = false;
                attackRight = false;
                movingRight = false;
                speed = normalSpeed;
            }
            else if (transform.position.x < leftWayPoint.position.x)
            {
                attackMode = false;
                attackRight = false;
                movingRight = true;
                speed = normalSpeed;
            }

            Debug.Log(attackMode + "\t" + attackRight + "\t" + movingRight + "\t" + speed);
            
            if(!attackMode)
            {
                Debug.Log("Not Attack Mode");
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
                Debug.Log("Attack Mode");
                if(attackRight == true)
                {
                    MoveAttackRight();
                }
                else
                {
                    MoveAttackLeft();
                }

                attackMode = false;
            }
        }
    }

    void MoveLeft()
    {
        anim.SetBool("isWalking", true);
        Debug.Log("MoveLeft");
        transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
        // transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector2(-4, 4);
        diamond.transform.position = transform.position;
    }

    void MoveRight()
    {
        anim.SetBool("isWalking", true);
        Debug.Log("MoveRight");
        transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        // transform.eulerAngles = new Vector3(0, 180, 0);
        transform.localScale = new Vector2(4, 4);
        diamond.transform.position = transform.position;
    }

    void MoveAttackLeft()
    {
        Debug.Log("AttackLeft");
        // transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector2(-4, 4);
        anim.SetTrigger("Attack");
        StartCoroutine(ShootBulletLeft(5f));
    }

    IEnumerator ShootBulletLeft(float delay)
    {
        Instantiate(projectileLeft, shotPoint.position, transform.rotation);

        yield return new WaitForSeconds(delay);
    }

    void MoveAttackRight()
    {
        Debug.Log("AttackRight");
        transform.localScale = new Vector2(4, 4);
        // transform.eulerAngles = new Vector3(0, 180, 0);
        anim.SetTrigger("Attack");
        StartCoroutine(ShootBulletRight(5f));
    }

    IEnumerator ShootBulletRight(float delay)
    {
        Instantiate(projectileRight, shotPoint.position, transform.rotation);

        yield return new WaitForSeconds(delay);
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
