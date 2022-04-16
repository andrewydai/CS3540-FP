using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehavior : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool paused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOrUnpause();
        }
    }

    public void PauseOrUnpause()
    {
        if (paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
        }

        paused = !paused;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Cursor.visible = !Cursor.visible;
    }
}
