using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1ObjectiveManager : MonoBehaviour
{
    public Text objectiveText;
    // Start is called before the first frame update
    void Awake()
    {
        EnemyBehavior.enemyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.isLevelOver)
        {
            return;
        }

        objectiveText.text = "Enemies Left: " + EnemyBehavior.enemyCount;
        if(EnemyBehavior.enemyCount == 0)
        {
            FindObjectOfType<LevelManager>().WinLevel();
        }
    }
}
