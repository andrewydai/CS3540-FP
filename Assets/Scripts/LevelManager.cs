using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool isLevelOver;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public Text statusText;
    public Text objectiveText;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        isLevelOver = false;
    }

    private void EndLevel(string msg, AudioClip endSFX)
    {
        isLevelOver = true;
        statusText.text = msg;
        AudioSource.PlayClipAtPoint(endSFX, Camera.main.transform.position);
    }

    public void WinLevel()
    {
        EndLevel("Kitchen Cleaned!", winSFX);
    }

    public void LoseLevel()
    {
        EndLevel("You Lost!", loseSFX);
        Invoke("ReloadLevel", 3);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Update()
    {
        objectiveText.text = "Enemies Left: " + EnemyBehavior.enemyCount;
    }
}
