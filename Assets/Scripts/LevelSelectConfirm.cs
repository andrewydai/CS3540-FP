using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectConfirm : MonoBehaviour
{
    public TMP_Text prompt;
    private string scene;

    public void UpdateLevel(string levelName, string sceneName)
    {
        scene = sceneName;
        prompt.text = $"{levelName} ?";
    }

    public void Confirm()
    {
        SceneManager.LoadScene(scene);
    }
}
