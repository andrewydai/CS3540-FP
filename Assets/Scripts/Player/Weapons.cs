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
    public Slider spongeSlider;

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
        spongeSlider.maxValue = spongeShootRate;

        activeWeapon = "broom";
        sponge.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spongeIcon.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponChange();
        Attack();
        SpongeReload();

        elapsedReloadTime += Time.deltaTime;
        elapsedShootTime += Time.deltaTime;
        DisplayCooldown();
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
        if (LevelManager.isLevelOver)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1")) {
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
            spongeSlider.value = 0;
        }
    }

    void SpongeReload()
    {
        if (spongeAmmoCurrent < spongeAmmoMax && spongeReloadRate <= elapsedReloadTime) {
            spongeAmmoCurrent += 1;
            elapsedReloadTime = 0f;
        }
    }
    
    void DisplayCooldown()
    {
        spongeSlider.value = Mathf.Clamp(elapsedShootTime, 0, spongeShootRate); 
    }
}
