using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float maxSpeed;

    [Header("Jump")]
    public float jumpPower;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundMask;
    public bool doubleGet;

    private Rigidbody2D rb;
    private float direction;
    private int jumpCount;

    private void Start()
    {
        jumpCount = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    private void jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void doubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        jumpCount++;
    }

    private void Update()
    {
        direction = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
        Debug.Log(isGrounded);
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }

        jump();

        if (Input.GetButtonDown("Jump") && !isGrounded && doubleGet && jumpCount < 1)
        {
            doubleJump();
        }

        if (isGrounded)
        {
            jumpCount = 0;
        }

    }

    private void FixedUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }



}
