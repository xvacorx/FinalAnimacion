using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    float jumpForce = 10f;
    float groundCheckRadius = 0.06f;

    private Rigidbody2D rb2D;
    private bool grounded;

    [SerializeField] GameObject foot;

    public bool lookingLeft;

    public bool isJumping;
    public float horizontalMovement;

    private int health = 3;
    private float score;
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI tmp1;

    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        {
            if (!grounded)
            {
                animator.SetFloat("yVelocity", rb2D.velocity.y);
                horizontalMovement = Input.GetAxis("Horizontal");
                rb2D.velocity = new Vector2(horizontalMovement * speed, rb2D.velocity.y);

                RotateTowardsMovementDirection(horizontalMovement);
            }
        } // Movement
        {
            IsGrounded();
            if (Input.GetButtonDown("Jump") && grounded)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
        } // Jump action
        animator.SetBool("isGrounded", grounded);
        tmp.text = "Score: " + score.ToString();
        tmp1.text = "Health: " + health.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce/2f);
            score += 5f;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            score += 10f;
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            LoseHealth();
        }
        if (collision.gameObject.CompareTag("Health"))
        {
            Destroy(collision.gameObject);
            health += 1;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("GameOverFinish");
        }
    }
    private void IsGrounded()
    {
        int layerMask = 1 << 7; // Only checks layer 7
        Collider2D[] colliders = Physics2D.OverlapCircleAll(foot.transform.position, groundCheckRadius, layerMask);
        grounded = colliders.Length > 0;
        isJumping = !grounded;
    } // Checks if the player is on land
    private void RotateTowardsMovementDirection(float horizontalMovement)
    {
        if (horizontalMovement < 0)
        {
            lookingLeft = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontalMovement > 0)
        {
            lookingLeft = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    } // Rotate player toward movement direction
    void LoseHealth()
    {
        health -= 1;
        if (health <= 0)
        {
            SceneManager.LoadScene("Level");
        }
    }
}