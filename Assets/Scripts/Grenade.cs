using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int currentGrenade = 0;
    public float throwForce = 5;
    private int[] count = { 3 };

    public GameObject dynamitePrefab;
    public Transform throwPoint;
    public GameManager gameManager;

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
            count[currentGrenade]--;
            gameManager.grenadeCount = count[currentGrenade];
        }

        if(currentGrenade == 0)//change if more grenade types added
        {
            gameManager.grenadeName = "Dynamite";
            gameManager.grenadeCount = count[currentGrenade];
        }
    }

    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(dynamitePrefab, throwPoint.position, transform.rotation);
        grenade.GetComponent<Rigidbody2D>().AddForce(throwPoint.up * throwForce, ForceMode2D.Impulse);
    }
}
