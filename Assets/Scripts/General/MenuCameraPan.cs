using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraPan : MonoBehaviour
{
    public float speed = .05f;
    public float radius = 15;
    public Transform target;

    private float timeCounter = 0;

    private void Update()
    {

        timeCounter += Time.deltaTime * speed;
        float x = Mathf.Cos(timeCounter) * radius + target.position.x;
        float z = Mathf.Sin(timeCounter) * radius + target.position.z;

        transform.LookAt(target);
        transform.position = new Vector3(x, transform.position.y, z);
    }

}
