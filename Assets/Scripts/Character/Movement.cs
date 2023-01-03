// https://v3x3d.itch.io/retro-lines


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Base Speed & Timer")]
    public float speed = 6.9f;
    public float jumpForce = 22f;
    public float jumpStartTime;
    private float jumpTime;
    public float rollForce = 22f;
    public static int rollingTimer = 45;

    [Header("Directional configuration")]
    public float horizontalInput;
    public float verticalInput;
    public static float facingDirection = 1;
    public bool lockDirection = false;

    [Header("GameObject Set")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;
    public LayerMask groundLayer;
    public GameObject groundCheck;
    public static GameObject backLayer;
    public GameObject StageBackground;
    public GameObject Camera;
    
    [Header("Authorization")]
    public static bool alreadyJumpedOnce = false;
    public static bool canJump = true;
    public static bool canRoll = true;
    public static bool isJumping = false;
    public static bool isRolling = false;
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
                isRolling= false;
                canJump = true;
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

        if(Input.GetAxis("FixDirection") > 0){
            lockDirection = true;
        } else {
            lockDirection = false;
        }

        //work background
        StageBackground.transform.position = new Vector3((Camera.transform.position.x /1.1f)+30f, (Camera.transform.position.y / 1.3f)+2f, 14.3f);

        if(haveControl){

            if(!isRolling){
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }
            

            Vector2 direction = new Vector2(horizontalInput, verticalInput);
            if(horizontalInput > 0){
                if(!lockDirection){
                    facingDirection = 1;
                    sr.flipX = false;
                }
                anim.SetBool("isRunning", true);
            } else if(horizontalInput < 0){
                if(!lockDirection){
                    facingDirection = -1;
                    sr.flipX = true;
                }
                anim.SetBool("isRunning", true);
            } else {
                anim.SetBool("isRunning", false);
            }

            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            if (GroundCheck.isGrounded && Input.GetButtonDown("Roll") && !isRolling && canRoll){
                if(horizontalInput > 0){
                    //canJump = false;
                    canRoll = false;
                    isRolling = true;
                    anim.SetBool("isLookingLeft", false);
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isRolling", true);
                    rollingTimer = 0;
                    speed = speed*2f;
                    //rb.AddForce(transform.right * rollForce, ForceMode2D.Impulse);
                } else if(horizontalInput < 0){
                    //canJump = false;
                    canRoll = false;
                    isRolling = true;
                    anim.SetBool("isLookingLeft", true);
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isRolling", true);
                    rollingTimer = 0;
                    speed = speed*2f;
                }
            }

            
            if (GroundCheck.isGrounded && Input.GetButtonDown("Jump") && canJump && !alreadyJumpedOnce){
                isJumping = true;
                jumpTime = jumpStartTime;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                alreadyJumpedOnce = true;
            }

            
            if(GroundCheck.isGrounded){
                anim.SetBool("isJumping", false);
            } else if(!GroundCheck.isGrounded && !isRolling) {
                anim.SetBool("isJumping", true);
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
