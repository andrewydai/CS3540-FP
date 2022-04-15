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
    public Text statusText;
    public Text objectiveText;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<AudioSource>().Play();
        isLevelOver = false;
        EnemyBehavior.enemyCount = 0;
    }

    private void EndLevel(string msg, AudioClip endSFX)
    {
        AudioSource.PlayClipAtPoint(endSFX, Camera.main.transform.position);
        statusText.text = msg;
        isLevelOver = true;
    }

    public void WinLevel()
    {
        EndLevel("Kitchen Cleaned!", winSFX);
    }

    public void LoseLevel()
    {
        EndLevel("You Lost!", loseSFX);
        Invoke("ReloadLevel", 4);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Update()
    {
        objectiveText.text = "Enemies Left: " + EnemyBehavior.enemyCount;
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
