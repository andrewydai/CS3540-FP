using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    // source of code and method
    // https://learn.unity.com/tutorial/implement-data-persistence-between-scenes
    public static PersistentData Instance;
    public int mouseSens = 5;
    public int playerLocation = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            mouseSens = Instance.mouseSens;
            playerLocation = Instance.playerLocation;
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
