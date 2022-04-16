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
    PersistentData settings;
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
    int roll;
    
    // for game dev usage
    public GameObject dataPrefab;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        cameraMount = GameObject.FindGameObjectWithTag("CameraMount");

        settings = PersistentData.Instance;
        // if debugging without going through menu, create new instance
        if (settings == null)
        {
            Instantiate(dataPrefab);
            settings = GameObject.FindGameObjectWithTag("Data").GetComponent<PersistentData>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitAnimStates();
    }

    // Update is called once per frame
    void Update()
    {
        var bgobj = GameObject.FindGameObjectWithTag("IsBossLevel");
        if (bgobj != null)
        {
            if (!PauseBehavior.paused && !BossLevelManager.isLevelOver)
            {
                NormalMoving();
                MouseRotations();
            }
        }
        else if (!PauseBehavior.paused && !LevelManager.isLevelOver)
        {
            NormalMoving();
            MouseRotations();
        }
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
        turn.x += Input.GetAxis("Mouse X") * settings.mouseSens;
        turn.y += Input.GetAxis("Mouse Y") * settings.mouseSens;
        turn.y = Mathf.Clamp(turn.y, pitchMin, pitchMax);

        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        cameraMount.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
    }
}
