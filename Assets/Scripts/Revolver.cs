using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour
{
    private int revolverAmmo, revolverMaxAmmo = 6;
    private float reloadTime = 1;
    private bool isReloading = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        revolverAmmo = revolverMaxAmmo;
        //UI Elements
        gameManager.weaponName = "Revolver";
        gameManager.maxAmmo = revolverMaxAmmo;
        gameManager.ammo = revolverAmmo;
    }

    private void OnEnable()
    {
        //UI Elements
        gameManager.weaponName = "Revolver";
        gameManager.maxAmmo = revolverMaxAmmo;
        gameManager.ammo = revolverAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        //Fires revolver
        if (Input.GetMouseButtonDown(0) && revolverAmmo > 0)
        {
            Fire();
            revolverAmmo--;
            gameManager.ammo = revolverAmmo;
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
        revolverAmmo = 0;
        yield return new WaitForSeconds(reloadTime);
        revolverAmmo = revolverMaxAmmo;
        gameManager.ammo = revolverAmmo;
        isReloading = false;
    }
}
