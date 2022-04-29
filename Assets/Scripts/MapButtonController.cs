using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapButtonController : MonoBehaviour
{
    public Slider[] paths;
    public int selfLocation;
    public float moveTime = 1f;
    public GameObject confirmModal;
    public string sceneName;
    public string levelName;

    private int playerLevel;

    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerLevel = PlayerPrefs.GetInt("playerLevel", 0);
        StartPlayerAtLevel();
        if (playerLevel == selfLocation)
        {
            confirmModal.GetComponent<LevelSelectConfirm>().UpdateLevel(levelName, sceneName);
        }
    }

    public void MovePlayer()
    {
        playerLevel = PlayerPrefs.GetInt("playerLevel");
        // don't move if clicked on same level
        if (playerLevel == selfLocation)
        {
            return;
        }

        if (playerLevel < selfLocation)
        {
            StartCoroutine(LeftToRight());
        }
        else
        {
            StartCoroutine(RightToLeft());
        }
        PlayerPrefs.SetInt("playerLevel", selfLocation);
    }

    IEnumerator LeftToRight()
    {
        for (int index = playerLevel; index < selfLocation; index++)
        {
            StartCoroutine(MoveAlongPath(index, 0, 1));
            yield return new WaitForSeconds(moveTime);
        }
        confirmModal.GetComponent<LevelSelectConfirm>().UpdateLevel(levelName, sceneName);
    }

    // copy of function above with reverse direction, too lazy to generalize sorry
    IEnumerator RightToLeft()
    {
        for (int index = playerLevel; index > selfLocation; index--)
        {
            StartCoroutine(MoveAlongPath(index - 1, 1, 0));
            yield return new WaitForSeconds(moveTime);
        }
        confirmModal.GetComponent<LevelSelectConfirm>().UpdateLevel(levelName, sceneName);
    }

    IEnumerator MoveAlongPath(int pathIndex, int start, int end)
    {
        Slider path = paths[pathIndex];
        // show current path and hide paths around it
        path.handleRect.gameObject.SetActive(true);
        if (pathIndex > 0)
        {
            paths[pathIndex - 1].handleRect.gameObject.SetActive(false);
        }
        if (pathIndex < paths.Length - 1)
        {
            paths[pathIndex + 1].handleRect.gameObject.SetActive(false);
        }
        float passedTime = 0f;
        while (passedTime < moveTime)
        {
            passedTime += Time.deltaTime;
            float lerpTime = passedTime / moveTime;
            path.value = Mathf.Lerp(start, end, lerpTime);
            yield return null;
        }
    }

    void StartPlayerAtLevel()
    {
        Slider path;
        if (playerLevel < paths.Length)
        {
            path = paths[playerLevel];
            path.value = 0;
        }
        else
        {
            path = paths[playerLevel - 1];
            path.value = 1;
        }
        path.handleRect.gameObject.SetActive(true);
    }
}
