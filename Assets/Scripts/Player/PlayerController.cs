using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity = 9.81f;
    public float jumpHeight = 5f;
    public float moveSpeed = 10f;
    Vector3 input, moveDirection;
    CharacterController _controller;
    public float airControl = 10f;
    Animator anim;

    int idle;
    int walkForward;
    int walkBackward;
    int jump;
    int attack;
    int strafeLeft;
    int strafeRight;
    int roll;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitAnimStates();
    }

    // Update is called once per frame
    void Update()
    {
        NormalMoving();
    }

    void InitAnimStates()
    {
        idle = 0;
        walkForward = 1;
        walkBackward = 2;
        strafeLeft = 3;
        strafeRight = 4;
        jump = 5;
        attack = 6;
        roll = 7;
    }

    void Roll()
    {
        // if the player is pressing roll
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetInteger("State", roll);
        }
    }

    void NormalMoving()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        input *= moveSpeed;
        
        // if we are on the ground
        if (_controller.isGrounded)
        {
            if (!Input.anyKey || anim.GetInteger("State") == jump ||  anim.GetInteger("State") == roll)
            { // idling
                anim.SetInteger("State", idle);
            }
            moveDirection = input;
            // if the player is pressing jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
                anim.SetInteger("State", jump);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else 
        {   
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }
        // set walking animations
        if (anim.GetInteger("State") != jump && anim.GetInteger("State") != roll) {
            if (moveVertical > 0 && moveHorizontal < 0.5 && moveHorizontal > -0.5) 
            { // walking forwards
                anim.SetInteger("State", walkForward);
                // handle rolling
                Roll();
            }
            else if (moveVertical < 0 && moveHorizontal < 0.5 && moveHorizontal > -0.5) 
            { // walking backwards
                anim.SetInteger("State", walkBackward);
            }
            else if (moveHorizontal < 0)
            { // strafing left
                anim.SetInteger("State", strafeLeft);
                // handle rolling
                Roll();
            }
            else if (moveHorizontal > 0)
            { // strafing right
                anim.SetInteger("State", strafeRight);
                // handle rolling
                Roll();
            }
        }
        // Debug.Log($"horizontal: {moveHorizontal}, vertical: {moveVertical}, grounded: {_controller.isGrounded}, dir: {moveDirection}");
        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);
    }
}
