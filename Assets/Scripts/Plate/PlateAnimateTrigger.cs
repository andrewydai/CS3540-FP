using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateAnimateTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleAttack()
    {
        gameObject.GetComponentInParent<PlateBehavior>().ToggleAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Hit player");
        }
    }
}
