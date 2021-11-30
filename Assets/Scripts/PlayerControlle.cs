using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlle : MonoBehaviour
{
    private float speed = 1.5f;
    private float jumpForce = 2f;
    private Vector2 moveVector;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool onGround;
    public Transform groundCheck;
    public LayerMask ground;
    private float groundCheckRadius = 0.1f;
    public Transform topCheck;
    private float topCheckRadius;
    public LayerMask roof;
    public Collider2D poseStand;
    public Collider2D poseCrouch;
    public Collider2D poseDeath;
    private bool jumpLock = false;
    private bool death = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        groundCheckRadius = groundCheck.GetComponent<CircleCollider2D>().radius;
        topCheckRadius = topCheck.GetComponent<CircleCollider2D>().radius;
    }
    private void Update()
    {
        Run();
        Flip();
        Jump();
        GroundCheck();
        CrouchCheck();
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "obs")
        {

            anim.SetBool("death", true);
            death = true;
            if (death)
            {
                poseStand.enabled = false;
                poseCrouch.enabled = false;
                poseDeath.enabled = true;
            }

            
            
            
            Invoke("Restart", 2f);
        }
    }
    
   
    // Update is called once per frame
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Run()
    {
        if (!death)
        {
            moveVector.x = Input.GetAxisRaw("Horizontal");
            anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        }
        
    }

    void Flip()
    {
        sprite.flipX = moveVector.x < 0;
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !jumpLock)
        {
            anim.SetBool("boolJump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
        }
        if (!onGround) 
        {
            anim.SetBool("boolJump", false);
        }
    }
    void GroundCheck()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        anim.SetBool("onGround", onGround);
        
    }
    void CrouchCheck()
    {
        if (Input.GetKey(KeyCode.LeftControl) && onGround)
        {
            speed = 1f;
            anim.SetBool("crouch", true);
            poseStand.enabled = false;
            poseCrouch.enabled = true;
            jumpLock = true;
        }
        else if (!Physics2D.OverlapCircle(topCheck.position, topCheckRadius, roof) && !death)
        {
            speed = 1.5f;
            anim.SetBool("crouch", false);
            poseStand.enabled = true;
            poseCrouch.enabled = false;
            jumpLock = false;
        }
    }
}
