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

    PlayerController player;
    string activeWeapon;

    public int spongeAmmoMax;
    int spongeAmmoCurrent;
    public float spongeReloadRate;
    float elapsedReloadTime;
    public float spongeShootRate = 1.3f;
    float elapsedShootTime;

    // Start is called before the first frame update
    void Start()
    {
        spongeAmmoCurrent = spongeAmmoMax;

        activeWeapon = "sponge";
        broom.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        broomIcon.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponChange();
        Attack();
        SpongeReload();

        elapsedReloadTime += Time.deltaTime;
        elapsedShootTime += Time.deltaTime;
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
                    BroomAttack();
                    break;
                case "sponge":
                    SpongeAttack();
                    break;
                default:
                    Debug.Log("no weapon equipped");
                    break;
            }
        }
    }

    void BroomAttack()
    {
        broom.GetComponentInChildren<BroomAttack>().SetAttacking();
        player.Attack();
    }

    void SpongeAttack()
    {
        if (spongeAmmoCurrent > 0 && spongeShootRate <= elapsedShootTime) {
            sponge.GetComponent<SpongeAttack>().Attack();
            player.Attack();

            spongeAmmoCurrent -= 1;
            elapsedShootTime = 0f;
        }
    }

    void SpongeReload()
    {
        if (spongeAmmoCurrent < spongeAmmoMax && spongeReloadRate <= elapsedReloadTime) {
            spongeAmmoCurrent += 1;
            elapsedReloadTime = 0f;
        }
    }
}
