using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private int shotgunAmmo, shotgunLoadedAmmo = 2, shotgunAmmoReserve = 16;
    private float reloadTime = 1;
    private float cooldown = .2f;
    private float switchTime = .5f;
    private bool isReloading = false;
    private bool isFireCooldown = false;
    private bool isSwitching = false;
    public int damage = 1;
    public int pellets = 6;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float spread = 5f;
    public float fireForce = 20f;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        shotgunAmmo = shotgunLoadedAmmo;
        //UI Elements
        gameManager.weaponName = "Shotgun";
        gameManager.maxAmmo = shotgunAmmoReserve;
        gameManager.ammo = shotgunAmmo;
        StartCoroutine(SwitchCooldown());
    }

    private void OnEnable()
    {
        //UI Elements
        gameManager.weaponName = "Shotgun";
        gameManager.maxAmmo = shotgunAmmoReserve;
        gameManager.ammo = shotgunAmmo;
        StartCoroutine(SwitchCooldown());
        isFireCooldown = false;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Fires revolver
        if (Input.GetMouseButtonDown(0) && shotgunAmmo > 0 && !isFireCooldown && !isReloading && !isSwitching)
        {
            for(int i = 0; i < pellets; i++)
                Fire();
            StartCoroutine(FireCooldown());
            shotgunAmmo--;
            gameManager.ammo = shotgunAmmo;
            gameManager.maxAmmo = shotgunAmmoReserve;
        }

        if (Input.GetKeyDown(KeyCode.R) && shotgunAmmoReserve > 0)
        {
            if (!isReloading)
                StartCoroutine(Reload());
        }
    }

    public void Fire()
    {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            Vector3 dir = firePoint.up + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
            bullet.GetComponent<Rigidbody2D>().AddForce(dir * fireForce, ForceMode2D.Impulse);
            bullet.GetComponent<MoveForward>().damage = damage;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        if (shotgunAmmoReserve > shotgunLoadedAmmo)
        {
            shotgunAmmoReserve = shotgunAmmoReserve + shotgunAmmo;
            shotgunAmmo = shotgunLoadedAmmo;
            shotgunAmmoReserve = shotgunAmmoReserve - shotgunLoadedAmmo;
        }
        else
        {
            shotgunAmmo = shotgunAmmoReserve;
            shotgunAmmoReserve = 0;
        }
        gameManager.ammo = shotgunAmmo;
        gameManager.maxAmmo = shotgunAmmoReserve;
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
