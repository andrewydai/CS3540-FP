using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonController : MonoBehaviour
{
    public Slider[] paths;
    public int selfLocation;
    public float moveTime = 1f;
    
    private PersistentData data;

    void Start()
    {
        data = PersistentData.Instance;
    }

    public void MovePlayer()
    {
        // don't move if clicked on same level
        if (data.playerLocation == selfLocation)
        {
            return;
        }

        if (data.playerLocation < selfLocation)
        {
            StartCoroutine(LeftToRight());
        }
        else
        {
            StartCoroutine(RightToLeft());
        }
    }

    IEnumerator LeftToRight()
    {
        for (int index = data.playerLocation; index < selfLocation; index++)
        {
            Slider nextPath = null;
            if (index < paths.Length - 1)
            {
                nextPath = paths[index + 1];
            }
            StartCoroutine(MoveAlongPath(paths[index], nextPath, 0, 1));
            yield return new WaitForSeconds(moveTime);
        }
        data.playerLocation = selfLocation;
    }

    // copy of function above with reverse direction, too lazy to generalize sorry
    IEnumerator RightToLeft()
    {
        for (int index = data.playerLocation; index > selfLocation; index--)
        {
            Slider nextPath = null;
            if (index > 1)
            {
                nextPath = paths[index - 2];
            }
            StartCoroutine(MoveAlongPath(paths[index - 1], nextPath, 1, 0));
            yield return new WaitForSeconds(moveTime);
        }
        data.playerLocation = selfLocation;
    }

    IEnumerator MoveAlongPath(Slider path, Slider nextPath, int start, int end)
    {
        float passedTime = 0f;
        while (passedTime < moveTime)
        {
            passedTime += Time.deltaTime;
            float lerpTime = passedTime / moveTime;
            path.value = Mathf.Lerp(start, end, lerpTime);
            yield return null;
        }
        if (nextPath)
        {
            path.handleRect.gameObject.SetActive(false);
            nextPath.handleRect.gameObject.SetActive(true);
        }
    }
}
