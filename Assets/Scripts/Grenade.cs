using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int currentGrenade = 0;
    public float throwForce = 5;
    private int[] count = { 3 };

    public GameObject dynamitePrefab;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentGrenade >= transform.childCount - 1)
                currentGrenade = 0;
            else
                currentGrenade++;
        }

        if (Input.GetMouseButtonDown(1) && count[currentGrenade] > 0)
        {
            ThrowGrenade();
        }
    }

    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(dynamitePrefab, transform.position, transform.rotation);
        grenade.GetComponent<Rigidbody2D>().AddForce(transform.up * throwForce, ForceMode2D.Impulse);
    }
}
