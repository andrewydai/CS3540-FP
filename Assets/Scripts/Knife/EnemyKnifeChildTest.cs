using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnifeChildTest : MonoBehaviour
{
    EnemyKnife parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<EnemyKnife>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WakeUp()
    {
        parent.WakeUp();
    }

    public void Attack()
    {
        parent.Attack();
    }
}
