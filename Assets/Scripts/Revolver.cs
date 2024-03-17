using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    private int revolverAmmo, revolverLoadedAmmo = 6, revolverAmmoReserve = 60;
    private float reloadTime = 1;
    public int damage = 3;
    private float cooldown = .2f;
    private float switchTime = .5f;
    private bool isReloading = false;
    private bool isFireCooldown = false;
    private bool isSwitching = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        revolverAmmo = revolverLoadedAmmo;
        //UI Elements
        gameManager.weaponName = "Revolver";
        gameManager.maxAmmo = revolverAmmoReserve;
        gameManager.ammo = revolverAmmo;
        StartCoroutine(SwitchCooldown());
    }

    private void OnEnable()
    {
        //UI Elements
        gameManager.weaponName = "Revolver";
        gameManager.maxAmmo = revolverAmmoReserve;
        gameManager.ammo = revolverAmmo;
        StartCoroutine(SwitchCooldown());
        isFireCooldown = false;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Fires revolver
        if (Input.GetMouseButtonDown(0) && revolverAmmo > 0 && !isFireCooldown && !isReloading && !isSwitching)
        {
            Fire();
            StartCoroutine(FireCooldown());
            revolverAmmo--;
            gameManager.ammo = revolverAmmo;
            gameManager.maxAmmo = revolverAmmoReserve;
        }

        if (Input.GetKeyDown(KeyCode.R) && revolverAmmoReserve > 0)
        {
            if (!isReloading)
                StartCoroutine(Reload());
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        bullet.GetComponent<MoveForward>().damage = damage;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        if(revolverAmmoReserve > revolverLoadedAmmo)
        {
            revolverAmmoReserve = revolverAmmoReserve + revolverAmmo;
            revolverAmmo = revolverLoadedAmmo;
            revolverAmmoReserve = revolverAmmoReserve - revolverLoadedAmmo;
        }
        else
        {
            revolverAmmo = revolverAmmoReserve;
            revolverAmmoReserve = 0;
        }
        gameManager.ammo = revolverAmmo;
        gameManager.maxAmmo = revolverAmmoReserve;
        isReloading = false;
    }

    IEnumerator FireCooldown()
    {
        isFireCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isFireCooldown = false;
    }

    IEnumerator SwitchCooldown()
    {
        isSwitching = true;
        yield return new WaitForSeconds(switchTime);
        isSwitching = false;
    }
}
