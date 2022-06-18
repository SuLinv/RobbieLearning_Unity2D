using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("移动参数")] 
    public float speed = 8f;
    public float crouchSpeedDivisor = 3f;

    [Header("跳跃参数")] 
    public float jumpForce = 6.3f;
    public float jumpHoldForce = 1.9f;
    public float jumpHoldDuration = 0.1f;
    public float crouchJumpBoost = 2.5f;

    float jumpTime;

    [Header("状态")] 
    public bool isCrouch;
    public bool isOnGround;
    public bool isJump;
    public bool isHeadBlocked;

    [Header("环境检测")] 
    public LayerMask groundLayer;
    public float footOffset = 0.4f;
    public float headClearance = 0.5f;
    public float groundDistance = 0.2f;
    
    //碰撞体积
    private Vector2 collStandSize;
    private Vector2 collStandOffset;
    private Vector2 collCrouchSize;
    private Vector2 collCrouchOffset;
    
    //按键设置
    private bool jumpPressed;
    private bool jumpHeld;
    private bool crouchHeld;

    float xVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        collStandSize = coll.size;
        collStandOffset = coll.offset;
        collCrouchSize = new Vector2(collStandSize.x, collStandSize.y * 0.5f);
        collCrouchOffset = new Vector2(collStandOffset.x, collStandOffset.y * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
        jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
    }

    private void FixedUpdate()
    {
        PhyscisCheck();
        GroundMovement();
        MidAirMovement();
    }

    void PhyscisCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, groundDistance, groundLayer);
        if (leftCheck || rightCheck)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        RaycastHit2D headCheck = Raycast(new Vector2(footOffset, coll.size.y), Vector2.up, headClearance, groundLayer);
        if (headCheck)
        {
            isHeadBlocked = true;
        }
        else
        {
            isHeadBlocked = false;
        }
    }
    
    void GroundMovement()
    {
        if (crouchHeld && !isCrouch && isOnGround)
        {
            Crouch();
        }else if (!crouchHeld && isCrouch && !isHeadBlocked)
        {
            StandUp();
        }else if (!isOnGround && isCrouch)
        {
            StandUp();
        }
        
        xVelocity = Input.GetAxis("Horizontal");

        if (isCrouch)
        {
            xVelocity /= crouchSpeedDivisor;
        }
        
        rb.velocity = new Vector2(speed * xVelocity, rb.velocity.y);
        
        PlayerMoveFaceTo();
    }

    void PlayerMoveFaceTo()
    {
        if (xVelocity < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (xVelocity > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Crouch()
    {
        isCrouch = true;
        coll.size = collCrouchSize;
        coll.offset = collCrouchOffset;
    }

    void StandUp()
    {
        isCrouch = false;
        coll.size = collStandSize;
        coll.offset = collStandOffset;
    }

    void MidAirMovement()
    {
        if (jumpPressed && isOnGround && !isJump)
        {
            if (isCrouch)
            {
                StandUp();
                rb.AddForce(new Vector2(0f,crouchJumpBoost),ForceMode2D.Impulse);
            }
            isJump = true;
            isOnGround = false;
            jumpPressed = false;
            jumpTime = Time.time + jumpHoldDuration;
            
            rb.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
        }else if (isJump)
        {
            if (jumpHeld)
            {
                rb.AddForce(new Vector2(0f,jumpHoldForce),ForceMode2D.Impulse);
            }

            if (jumpTime < Time.time)
            {
                isJump = false;
            }
        }
    }

    RaycastHit2D Raycast(Vector2 startOffset,Vector2 rayDirection,float length,LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + startOffset, rayDirection, length, layer);
        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(pos+startOffset,rayDirection,color);
        return hit;
    }
}
