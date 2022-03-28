using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    public GameObject broom;
    public GameObject sponge;
    public Text weaponText;

    PlayerController player;
    string activeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = "sponge";
        broom.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        if (Input.GetButtonDown("Fire1") && !LevelManager.isLevelOver) {
            switch (activeWeapon) {
                case "broom":
                    broom.GetComponentInChildren<BroomAttack>().SetAttacking();
                    player.Attack();
                    break;
                case "sponge":
                    sponge.GetComponent<SpongeAttack>().Attack();
                    player.Attack();
                    break;
                default:
                    Debug.Log("no weapon equipped");
                    break;
            }
        }
    }
}
