using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float pitchMin = -60f;
    public float pitchMax = 25f;
    float pitch = 0f;
    Transform playerBody;
    PersistentData settings;

    // for game dev usage
    public GameObject dataPrefab;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerBody = transform.parent.transform;
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
        pitch = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * settings.mouseSens * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * settings.mouseSens * Time.deltaTime;

        // yaw
        playerBody.Rotate(Vector3.up * moveX);

        // pitch
        pitch = Mathf.Clamp(pitch - moveY, pitchMin, pitchMax);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
