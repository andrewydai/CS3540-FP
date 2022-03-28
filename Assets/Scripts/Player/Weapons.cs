using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    public GameObject broom;
    public GameObject sponge;
    public Image broomIcon;
    public Image spongeIcon;

    string activeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = "sponge";
        broom.SetActive(false);
        broomIcon.color = Color.gray;
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
            broomIcon.color = Color.white;
            sponge.SetActive(false);
            spongeIcon.color = Color.gray;
        }

        if (Input.GetKeyDown("2")) {
            activeWeapon = "sponge";
            broom.SetActive(false);
            broomIcon.color = Color.gray;
            sponge.SetActive(true);
            spongeIcon.color = Color.white;
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !LevelManager.isLevelOver) {
            switch (activeWeapon) {
                case "broom":
                    broom.GetComponent<BroomAttack>().Attack();
                    break;
                case "sponge":
                    sponge.GetComponent<SpongeAttack>().Attack();
                    break;
                default:
                    Debug.Log("no weapon equipped");
                    break;
            }
        }
    }
}
