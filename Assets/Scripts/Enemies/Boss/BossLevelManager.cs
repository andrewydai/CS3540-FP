using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossLevelManager : MonoBehaviour
{
    public static bool isLevelOver;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public Text statusText;
    public Slider bossHealth;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<AudioSource>().Play();
        isLevelOver = false;
        bossHealth.maxValue = FindObjectOfType<BossBehavior>().bossHealth;
        if (PersistentData.Instance.mouseSens == 0)
        {
            PersistentData.Instance.mouseSens = 5;
            ChangeMouseSens mouseSens = GameObject.FindGameObjectWithTag("MouseSens").GetComponent<ChangeMouseSens>();
            mouseSens.sensSlider.value = 5;
            mouseSens.sensInput.text = "5";
        }
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
        EndLevel("Boss Defeated!", winSFX);
    }

    public void LoseLevel()
    {
        EndLevel("You Lost!", loseSFX);
        Invoke("ReloadLevel", 4);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene("Level1_boss");
    }

    private void Update()
    {
        if (!isLevelOver)
        {
            int currentBossHealth = FindObjectOfType<BossBehavior>().currentHealth;
            if (currentBossHealth <= 0)
            {
                WinLevel();
                bossHealth.value = 0;
            }
            else
            {
                bossHealth.value = currentBossHealth;
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        if (PauseBehavior.paused)
        {
            PauseBehavior.paused = false;
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(levelName);
    }
}
