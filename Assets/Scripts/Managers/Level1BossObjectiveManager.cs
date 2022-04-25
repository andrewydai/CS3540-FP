using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1BossObjectiveManager : MonoBehaviour
{
    public Slider bossHealth;
    // Start is called before the first frame update
    void Awake()
    {
        bossHealth.maxValue = FindObjectOfType<BossBehavior>().bossHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isLevelOver)
        {
            return;
        }

        int currentBossHealth = FindObjectOfType<BossBehavior>().currentHealth;
        if (currentBossHealth <= 0)
        {
            FindObjectOfType<BossBehavior>().Death();
            FindObjectOfType<LevelManager>().WinLevel();
            bossHealth.value = 0;
        }
        else
        {
            bossHealth.value = currentBossHealth;
        }
    }
}
