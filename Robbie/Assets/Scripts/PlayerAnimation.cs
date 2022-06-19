using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerMovement playerMovement;

    private int speedID;
    private int groundID;
    private int hangingID;
    private int crouchID;
    private int fallID;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();

        speedID = Animator.StringToHash("speed");
        groundID = Animator.StringToHash("isOnGround");
        hangingID = Animator.StringToHash("isHanging");
        crouchID = Animator.StringToHash("isCrouching");
        fallID = Animator.StringToHash("verticalVelocity");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(speedID,Mathf.Abs(playerMovement.xVelocity));
        anim.SetBool(groundID,playerMovement.isOnGround);
        anim.SetBool(hangingID,playerMovement.isHanging);
        anim.SetBool(crouchID, playerMovement.isCrouch);
        anim.SetFloat(fallID,rb.velocity.y);
    }

    public void StepAudio()
    {
        AudioManage.PlayFootStepAudio();
    }

    public void CrouchStepAudio()
    {
        AudioManage.PlayCrouchFootStepAudio();
    }
}
