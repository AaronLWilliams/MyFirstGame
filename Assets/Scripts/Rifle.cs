using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    private int rifleAmmo, rifleLoadedAmmo = 6,rifleAmmoReserve = 36;
    private float reloadTime = 1f;
    private float cooldown = .7f;
    private float switchTime = .5f;
    private bool isReloading = false;
    private bool isFireCooldown = false;
    private bool isSwitching = false;
    public int damage = 5;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rifleAmmo = rifleLoadedAmmo;
        //UI Elements
        gameManager.weaponName = "Rifle";
        gameManager.maxAmmo = rifleAmmoReserve;
        gameManager.ammo = rifleAmmo;
        StartCoroutine(SwitchCooldown());
    }

    private void OnEnable()
    {
        //UI Elements
        gameManager.weaponName = "Rifle";
        gameManager.maxAmmo = rifleAmmoReserve;
        gameManager.ammo = rifleAmmo;
        StartCoroutine(SwitchCooldown());
        isFireCooldown = false;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Fires revolver
        if (Input.GetMouseButtonDown(0) && rifleAmmo > 0 && !isFireCooldown && !isReloading && !isSwitching)
        {
            Fire();
            StartCoroutine(FireCooldown());
            rifleAmmo--;
            gameManager.ammo = rifleAmmo;
            gameManager.maxAmmo = rifleAmmoReserve;
        }

        if (Input.GetKeyDown(KeyCode.R) && rifleAmmoReserve > 0)
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
        if (rifleAmmoReserve > rifleLoadedAmmo)
        {
            rifleAmmoReserve = rifleAmmoReserve + rifleAmmo;
            rifleAmmo = rifleLoadedAmmo;
            rifleAmmoReserve = rifleAmmoReserve - rifleLoadedAmmo;
        }
        else
        {
            rifleAmmo = rifleAmmoReserve;
            rifleAmmoReserve = 0;
        }
        gameManager.ammo = rifleAmmo;
        gameManager.maxAmmo = rifleAmmoReserve;
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
