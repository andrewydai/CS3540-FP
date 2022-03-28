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

    int idle1;
    int idle2;
    int walkForward;
    int walkBackward;
    int jump;
    int attack;

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
        idle1 = 0;
        idle2 = 1;
        walkForward = 2;
        walkBackward = 3;
        jump = 4;
        attack = 5;
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
            anim.SetInteger("State", Random.Range(0, 2));
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
        // set walking and idle animations
        if (anim.GetInteger("State") != jump) {
            if (input.x != 0 || input.z != 0) {
                anim.SetInteger("State", walkForward);
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);
    }
}
