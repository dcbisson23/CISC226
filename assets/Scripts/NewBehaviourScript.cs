using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : TimeControlled
{
    
    private float jumpVelocity = 30;
    private float moveSpeed = 2;
    private float dashForce = 5;

    public bool floating;
    private bool crouch;
    private bool facingR; 
    private bool dashReady;
    private bool jumpReady;

    public AnimationClip idleAnimation;
    public AnimationClip walkingAnimation;
    public AnimationClip flyingAnimation;

    public Animator animator;
    private SpriteRenderer spi; 
    private EdgeCollider2D groundCollider;
    private Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<EdgeCollider2D>();
        spi = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        floating = false;
        facingR = true;  
        dashReady = true;
        jumpReady = true;

    }

    // Update is called once per frame
    public override void TimeUpdate()
    {   
        base.TimeUpdate();
        // Animator update
        // animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        // animator.SetBool("inAir", floating);
        
        Vector2 pos = transform.position;

        pos.y += velocity.y * Time.deltaTime;
        velocity.y += TimeController.gravity * Time.deltaTime;
        if (facingR)
        {
        pos.x += velocity.x * Time.deltaTime;
        }
        if (!facingR)
        {
            pos.x -= velocity.x *Time.deltaTime;
        }
        if (velocity.x > 0){velocity.x -= 3 * Time.deltaTime;}
        

        
        if (pos.y < 1)
        {
            pos.y = 1;
            velocity.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.W) && jumpReady == true)
        {   
            // rb2d.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            velocity.y = jumpVelocity;
            // floating = true;
            jumpReady = false;   
        }
        if (Input.GetKey(KeyCode.A))
        {   
            if (facingR)
            {
                facingR = false;
                spi.flipX = true;
            }
            velocity.x = moveSpeed;
            pos.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!facingR)
            {
                facingR = true;
                spi.flipX = false;    
            }
            velocity.x = moveSpeed;
            pos.x += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && facingR && dashReady)
        {
            pos.x += dashForce;
            dashReady = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !facingR && dashReady)
        {
            pos.x -= dashForce;
            dashReady = false;
        }
        // if (velocity.x > 1 && !floating)
        // {
        //     currentAnimation = walkingAnimation;
        // }
        if (!Input.anyKey){
            currentAnimation = idleAnimation;
        }
        // if (floating) 
        // {
        //     currentAnimation = flyingAnimation;
        // }

        transform.position = pos;

        
    }    
    // Triggers
    private void OnTriggerEnter2D(Collider2D groundCollider) {
        floating = false;
        dashReady = true;
        jumpReady = true; 
        TimeController.gravity = 0;
        currentAnimation = walkingAnimation;
        velocity.y = 0;
        if (velocity.x > 0){ currentAnimation = walkingAnimation;}
        else{ currentAnimation = idleAnimation;}
    }
    private void OnTriggerStay2D(Collider2D groundCollider) {
        floating = false;
        dashReady = true;
        jumpReady = true;
        if (velocity.x > 0){ currentAnimation = walkingAnimation;}
        else{ currentAnimation = idleAnimation;}
    }
    private void OnTriggerExit2D(Collider2D groundCollider) {
        jumpReady = false; 
        TimeController.gravity = -100;
        floating = true;
        currentAnimation = flyingAnimation;
    }
}
