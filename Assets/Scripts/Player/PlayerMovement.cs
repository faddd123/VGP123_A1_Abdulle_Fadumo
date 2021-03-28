using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer kirbySprite;

    public float Speed;
    public int jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

     int _score = 0;
     public int score
     {
         get { return _score; }
         set
         {
             _score = value;
             Debug.Log("Current score is " + _score);
         }
     }

    public int maxLives = 3;
    int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
            else if (_lives < 0)
            {
                // run game over code here
            }
            Debug.Log("Current Lives are " + _lives);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        kirbySprite = GetComponent<SpriteRenderer>();

        if (Speed <= 0)
        {
            Speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 100;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.01f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please set a transform value for groundcheck");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);

        }

        rb.velocity = new Vector2(horizontalInput * Speed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", isGrounded);

        if (kirbySprite.flipX && horizontalInput > 0 || !kirbySprite.flipX && horizontalInput < 0)
            kirbySprite.flipX = !kirbySprite.flipX;

    }
    /* public void startJumpForceChange()
    {
        startCoroutine(JumpForceChange());
    }

    IEnumerator JumpForceChange()
    {
        jumpForce = 200;
        yield return new WaitForSeconds(2.0f);
        jumpForce = 150;
    }
    */

    void onTriggerStay2D(Collider2D collision)

    {
        if (collision.gameObject.tag == "Pickups")
        {

            Pickups curPickup = collision.GetComponent<Pickups>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (curPickup.currentCollectible)
                {
                    case Pickups.CollectibleType.KEY:
                        Destroy(collision.gameObject);
                        // add to inventory
                        break;
                }
            }

        }
    }
}
