using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoldTouch : MonoBehaviour
{
    public int freezeTime;
    public Material goldMat;
    Material playerMaterial;
    Weapons weaponScript;
    PlayerController controllerScript;
    Animator anim;
    Renderer mr;
    void Awake()
    {
        mr = GetComponentInChildren<Renderer>();
        playerMaterial = mr.material;
        anim = GetComponent<Animator>();
        weaponScript = GetComponent<Weapons>();
        controllerScript = GetComponent<PlayerController>();
    }
    public void Freeze()
    {
       weaponScript.enabled = false;
       controllerScript.isFrozen = true;
       mr.material = goldMat;
       anim.SetInteger("State", -1);
       StartCoroutine(Unfreeze());
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(freezeTime);
        weaponScript.enabled = true;
        controllerScript.isFrozen = false;
        mr.material = playerMaterial;
        anim.SetInteger("State", 0);
        yield return null;
    }
}
