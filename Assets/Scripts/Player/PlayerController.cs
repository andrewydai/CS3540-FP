using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isFrozen;
    public float gravity = 9.81f;
    public float jumpHeight = 5f;
    public float moveSpeed = 10f;
    Vector3 input, moveDirection;
    CharacterController _controller;
    public float airControl = 10f;
    Animator anim;

    GameObject cameraMount;
    Vector2 turn;
    public float pitchMin = -30f;
    public float pitchMax = 30f;

    int idle;
    int walkForward;
    int walkBackward;
    int jump;
    int attack;
    int strafeLeft;
    int strafeRight;

    void Awake()
    {
        isFrozen = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        cameraMount = GameObject.FindGameObjectWithTag("CameraMount");
    }

    // Start is called before the first frame update
    void Start()
    {
        InitAnimStates();
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.isLevelOver || PauseBehavior.paused)
        {
            return;
        }

        NormalMoving();
        MouseRotations();
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
    }

    public void Attack()
    {
        anim.SetInteger("State", attack);
    }

    void Idle()
    {
        anim.SetInteger("State", idle);
    }

    void NormalMoving()
    {
        if(isFrozen)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            _controller.Move(moveDirection * Time.deltaTime);
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        input *= moveSpeed;

        if (input.magnitude > 0.01f) {
            float cameraYawRotation = Camera.main.transform.eulerAngles.y;
            Quaternion newRotation = Quaternion.Euler(0, cameraYawRotation, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10);
        }
        
        // if we are on the ground
        if (_controller.isGrounded)
        {
            if (!Input.anyKey || anim.GetInteger("State") == jump)
            { // idling
                Idle();
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
        if (anim.GetInteger("State") != jump && anim.GetInteger("State") != attack) {
            if (moveVertical > 0 && moveHorizontal < 0.5 && moveHorizontal > -0.5) 
            { // walking forwards
                anim.SetInteger("State", walkForward);
            }
            else if (moveVertical < 0 && moveHorizontal < 0.5 && moveHorizontal > -0.5) 
            { // walking backwards
                anim.SetInteger("State", walkBackward);
            }
            else if (moveHorizontal < 0)
            { // strafing left
                anim.SetInteger("State", strafeLeft);
            }
            else if (moveHorizontal > 0)
            { // strafing right
                anim.SetInteger("State", strafeRight);
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);
    }

    void MouseRotations()
    {
        if(isFrozen)
        {
            return;
        }
        turn.x += Input.GetAxis("Mouse X") * LevelManager.mouseSens;
        turn.y += Input.GetAxis("Mouse Y") * LevelManager.mouseSens;
        turn.y = Mathf.Clamp(turn.y, pitchMin, pitchMax);

        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        cameraMount.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
    }
}
