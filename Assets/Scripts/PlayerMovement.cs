using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    public int health = 150;
    public int score = 0;
    public Text healthText;
    public Text diamondText;
    public GameObject gameOver;
    public GameObject levelPassed;

    int maxAliens;
    public int numAliens;

    // public GameObject gate;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        maxAliens = 4;
        numAliens = 4;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // gate.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HEALTH: " + health;
        diamondText.text = "DIAMONDS: " + score;
        if (health > 0)
        {
            Move();
            Jump();
            CheckIfGrounded();
            CheckGate();
        }
        else
        {
            RestartLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            health -= 3;

            if (health <= 0)
            {
                health = 0;
                gameOver.SetActive(true);

            }
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        if (x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if(x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void Jump()
    {
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            anim.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // health -= 2;
        } else
        {
            anim.SetBool("isJumping", false);
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if(collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    void CheckGate()
    {
        /* if(gate.GetComponent<SpriteRenderer>().enabled == false && numAliens <= (maxAliens / 2))
        {
            Debug.Log(maxAliens + "\t" + numAliens);
            gate.GetComponent<SpriteRenderer>().enabled = true;
        }
        if(gate.GetComponent<SpriteRenderer>().enabled == true && Vector3.Distance(transform.position, gate.transform.position) <= 1f)
        {
            levelPassed.SetActive(true);
        } */

        if(numAliens <= 0)
        {
            levelPassed.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        // gate.GetComponent<SpriteRenderer>().enabled = false;
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu()
    {
        // gate.GetComponent<SpriteRenderer>().enabled = false;
        SceneManager.LoadScene("Menu");
    }
}
