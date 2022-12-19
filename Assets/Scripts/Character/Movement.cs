// https://v3x3d.itch.io/retro-lines


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Base Speed & Timer")]
    public float speed = 6.9f;
    public float jumpForce = 22f;
    public float rollForce = 22f;
    public static int rollingTimer = 14;

    [Header("Directional configuration")]
    public float horizontalInput;
    public float verticalInput;
    public static float facingDirection = 1;

    [Header("GameObject Set")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;
    public LayerMask groundLayer;
    public GameObject groundCheck;
    public static GameObject backLayer;
    
    [Header("Authorization")]
    public static bool canJump = true;
    public static bool canRoll = true;
    public static bool isRolling = false;
    public static bool isDead = false;
    public static bool canMove = true;
    public static bool haveControl = true;
    
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

    }

    void FixedUpdate()
    {
        if(haveControl){
            if(rollingTimer >= 45){
                canRoll = true;
            } else if(rollingTimer >= 14){
                canRoll = false;
                canJump = true;
                isRolling= false;
                speed = 6.9f;
                anim.SetBool("isRolling", false);
                rollingTimer++;
            }else {
                canRoll = false;
                rollingTimer++;
            }
        }
    }

    void Update()
    {
        if(haveControl){

            if(!isRolling){
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }
            

            Vector2 direction = new Vector2(horizontalInput, verticalInput);
            if(horizontalInput > 0){
                facingDirection = 1;
                sr.flipX = false;
                anim.SetBool("isRunning", true);
            } else if(horizontalInput < 0){
                facingDirection = -1;
                sr.flipX = true;
                anim.SetBool("isRunning", true);
            } else {
                anim.SetBool("isRunning", false);
            }

            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            if (GroundCheck.isGrounded && Input.GetButtonDown("Jump") && canJump){
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if(GroundCheck.isGrounded){
                anim.SetBool("isJumping", false);
            } else {
                anim.SetBool("isJumping", true);
            }

            if (GroundCheck.isGrounded && Input.GetButtonDown("Roll") && !isRolling && canRoll){
                if(horizontalInput > 0){
                    canJump = false;
                    canRoll = false;
                    isRolling = true;
                    anim.SetBool("isRolling", true);
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isFalling", false);
                    anim.SetBool("isRunning", false);
                    rollingTimer = 0;
                    speed = speed*2f;
                    //rb.AddForce(transform.right * rollForce, ForceMode2D.Impulse);
                } else if(horizontalInput < 0){
                    canJump = false;
                    canRoll = false;
                    isRolling = true;
                    anim.SetBool("isRolling", true);
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isFalling", false);
                    anim.SetBool("isRunning", false);
                    rollingTimer = 0;
                    speed = speed*2f;
                }
            }

        }

        PreventExtreme();
    }

    void PreventExtreme(){

        if(!isRolling && rb.velocity.x > 6.7f) {
            rb.velocity = new Vector2(6.7f, rb.velocity.y);
        }
        if(!isRolling && rb.velocity.x < -6.7f) {
            rb.velocity = new Vector2(-6.7f, rb.velocity.y);
        }
        if(rb.velocity.y > 30f) {
            rb.velocity = new Vector2(rb.velocity.x, 30f);
        }
        if(rb.velocity.y < -30f) {
            rb.velocity = new Vector2(rb.velocity.x, -30f);
        }
    }


}
