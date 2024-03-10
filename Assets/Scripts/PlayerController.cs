using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] int maxHealth = 3;
    private int revolverAmmo, revolverMaxAmmo = 6;
    private float reloadTime = 1;
    private bool isReloading = false;

    public Rigidbody2D rb;
    public Weapon weapon;
    public GameManager gameManager;
    public GameObject player;

    Vector2 movementDirection;
    Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<health>().maxHealth = maxHealth;
        revolverAmmo = revolverMaxAmmo;
        //UI Elements
        gameManager.maxAmmo = revolverMaxAmmo;
        gameManager.ammo = revolverAmmo;
        gameManager.health = player.GetComponent<health>().currentHealth;
        

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Fires revolver
        if (Input.GetMouseButtonDown(0) && revolverAmmo > 0)
        {
            weapon.Fire();
            revolverAmmo--;
            gameManager.ammo = revolverAmmo;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(!isReloading)
                StartCoroutine(Reload());
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * moveSpeed;

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
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
