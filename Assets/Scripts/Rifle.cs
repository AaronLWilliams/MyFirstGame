using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    private int rifleAmmo, rifleLoadedAmmo = 6,rifleAmmoReserve = 36;
    private float reloadTime = 1;
    private bool isReloading = false;
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
        gameManager.maxAmmo = rifleLoadedAmmo;
        gameManager.ammo = rifleAmmo;
    }

    private void OnEnable()
    {
        //UI Elements
        gameManager.weaponName = "Rifle";
        gameManager.maxAmmo = rifleAmmoReserve;
        gameManager.ammo = rifleAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        //Fires revolver
        if (Input.GetMouseButtonDown(0) && rifleAmmo > 0)
        {
            Fire();
            rifleAmmo--;
            gameManager.ammo = rifleAmmo;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading)
                StartCoroutine(Reload());
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        rifleAmmo = 0;
        yield return new WaitForSeconds(reloadTime);
        rifleAmmo = rifleLoadedAmmo;
        gameManager.ammo = rifleAmmo;
        isReloading = false;
    }
}
