using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    float pitch = 0;
    float yaw = 0;
    public Transform playerHead;
    public Transform playerBody;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //yaw
        yaw += moveX;
        yaw = Mathf.Clamp(yaw, -30, 30);
        playerBody.Rotate(Vector3.up * moveX);

        //pitch
        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, -20, 20);

        //apply pitch and yaw
        playerHead.localRotation = Quaternion.Euler(-yaw, pitch, 0);

        /*
        Quaternion pHeadRotation = playerHead.rotation;
        Quaternion hybridRotation = playerBody.rotation;
        hybridRotation.y = playerHead.rotation.y;
        playerBody.localRotation = Quaternion.RotateTowards(playerBody.rotation, playerHead.rotation, Time.deltaTime * 2);
        playerHead.rotation = pHeadRotation;
        */
    }
}
