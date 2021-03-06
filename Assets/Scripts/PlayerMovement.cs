using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;
    
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState { idle, running, jumping, falling }
    private int jumpTimes = 0;

    [SerializeField] private AudioSource jumpSoundEffect; 

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        // if (dirX != 0) {
        //     Debug.Log(dirX);
        // }
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            jumpTimes = 1;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            jumpSoundEffect.Play();
        } else if (Input.GetButtonDown("Jump") && jumpTimes == 1) {
            jumpTimes++;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            jumpSoundEffect.Play();
        } 

        AnimUpdate();
    }

    private void AnimUpdate() 
    {
        MovementState state = MovementState.idle;
        
        if (dirX > 0f) {
            state = MovementState.running;
            sprite.flipX = false;
        } else if (dirX < 0f) {
            state = MovementState.running;
            sprite.flipX = true;
        } else {
            state = MovementState.idle;
        }
        
        if (rb.velocity.y > 0.01f) {
            state = MovementState.jumping;
        } else if (rb.velocity.y < -0.01f) {
            state = MovementState.falling;
        }
        
        anim.SetInteger("state",(int) state);
    }

    private bool IsGrounded() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f , jumpableGround);
    }
}
