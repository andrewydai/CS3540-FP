using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float mouseSensitivity = 50f;
    public float pitchMin = -60f;
    public float pitchMax = 25f;
    float pitch = 0f;
    Transform playerBody;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerBody = transform.parent.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        pitch = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // yaw
        playerBody.Rotate(Vector3.up * moveX);

        // pitch
        pitch = Mathf.Clamp(pitch - moveY, pitchMin, pitchMax);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
