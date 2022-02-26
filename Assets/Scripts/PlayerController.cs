using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float gravity = 9.81f;
    public float jumpHeight = 5f;

    Vector3 input;
    Vector3 moveDirection;
    CharacterController _controller;
    Animator _animator;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Animate();
        Move();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        //transform.Translate(input);
        input *= moveSpeed;

        if (_controller.isGrounded) {
            //jump
            moveDirection = input;
            if (Input.GetButton("Jump")) {
                //jump
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            } else {
                //ground
                moveDirection.y = 0.0f;
            }
        } else {
            //midair
        }

        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);
    }

    void Animate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool moveJump = Input.GetButton("Jump");

        if (moveJump) {
            _animator.Play("Standing_Jump", 3, 0.0f);
        } else if ((moveHorizontal != 0) || (moveVertical != 0)) {
            _animator.Play("Walk_Static", 0, 0.0f);
        }
        _animator.Play("Idle", 0, 0.0f);


    }
}