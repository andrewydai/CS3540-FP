using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isLevelOver;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public string winMessage;
    public string loseMessage;
    public string currentLevel;
    public string nextLevel;
    public Text statusText;
    
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<AudioSource>().Play();
        isLevelOver = false;
    }

    private void EndLevel(string msg, AudioClip endSFX)
    {
        GetComponent<AudioSource>().Stop();
        AudioSource.PlayClipAtPoint(endSFX, Camera.main.transform.position);
        statusText.text = msg;
        isLevelOver = true;
    }

    public void WinLevel()
    {
        EndLevel(winMessage, winSFX);
        Invoke("LoadNextLevel", 4);
    }

    public void LoseLevel()
    {
        EndLevel(loseMessage, loseSFX);
        Invoke("ReloadLevel", 4);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    private void LoadNextLevel()
    {
        if(nextLevel != "")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    public void LoadLevel(string levelName)
    {
        if(PauseBehavior.paused)
        {
            PauseBehavior.paused = false;
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(levelName);
    }
}
