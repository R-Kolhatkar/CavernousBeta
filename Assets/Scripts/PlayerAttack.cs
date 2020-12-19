using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttack;
    public LayerMask whatIsEnemies;
    public float startTimeBetweenAttack;

    public Transform attackPos;
    public float attackRange;

    // public int damage;

    Animator anim;

    public GameObject projectile;
    public Transform shotPoint;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenAttack <= 0)
        {
            // Player uses Melee Weapon (pickaxe)
            if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetMouseButtonDown(1))
            {
                anim.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyMovement>().TakeDamage(1);
                }
            }
            // Player uses Range Weapon (Revolver)
            else if(Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Shoot");
                Instantiate(projectile, shotPoint.position, transform.rotation);
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
