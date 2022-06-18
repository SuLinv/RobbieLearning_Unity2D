using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("移动参数")] 
    public float speed = 8f;
    public float crouchSpeedDivisor = 3f;

    [Header("状态")] 
    public bool isCrouch;

    float xVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GroundMovement();
    }

    void GroundMovement()
    {
        xVelocity = Input.GetAxis("Horizontal");
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
        
    }

    void StandUp()
    {
        
    }
}
