using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject broom;
    public GameObject sponge;

    string activeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = "broom";
        sponge.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        WeaponChange();
        Attack();
    }

    void WeaponChange()
    {
        if (Input.GetKeyDown("1")) {
            activeWeapon = "broom";
            broom.SetActive(true);
            sponge.SetActive(false);
        }

        if (Input.GetKeyDown("2")) {
            activeWeapon = "sponge";
            broom.SetActive(false);
            sponge.SetActive(true);
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1")) {
            switch (activeWeapon) {
                case "broom":
                    Debug.Log("broom attack");
                    break;
                case "sponge":
                    Debug.Log("sponge attack");
                    break;
                default:
                    Debug.Log("no weapon equipped");
                    break;
            }
        }
    }
}
