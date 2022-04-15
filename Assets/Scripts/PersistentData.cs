using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    // source of code and method
    // https://learn.unity.com/tutorial/implement-data-persistence-between-scenes
    public static PersistentData Instance;
    public int mouseSens = 100;

    private void Awake()
    {
        if (Instance != null)
        {
            mouseSens = Instance.mouseSens;
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
