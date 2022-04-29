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
            StartCoroutine(MoveAlongPath(index, 0, 1));
            yield return new WaitForSeconds(moveTime);
        }
        data.playerLocation = selfLocation;
    }

    // copy of function above with reverse direction, too lazy to generalize sorry
    IEnumerator RightToLeft()
    {
        for (int index = data.playerLocation; index > selfLocation; index--)
        {
            StartCoroutine(MoveAlongPath(index - 1, 1, 0));
            yield return new WaitForSeconds(moveTime);
        }
        data.playerLocation = selfLocation;
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
}
