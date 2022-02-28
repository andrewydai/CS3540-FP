using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    public GameObject broom;
    public GameObject sponge;
    public Text weaponText;

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
        weaponText.text = "Weapon: " + activeWeapon; 
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
        if (Input.GetButtonDown("Fire1") && !FindObjectOfType<LevelManager>().isLevelOver) {
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
