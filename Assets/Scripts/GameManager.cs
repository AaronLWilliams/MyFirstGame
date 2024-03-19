using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public int health;
    public int ammo;
    public int maxAmmo;
    public int grenadeCount;
    public string weaponName;
    public string grenadeName;
    public TMP_Text WeaponText;
    public TMP_Text AmmoText;
    public TMP_Text GrenadeText;
    public TMP_Text GrenadeCountText;
    public TMP_Text HealthText;
    public GameObject player;

    List<Zombie> enemies = new List<Zombie>();

    private void Awake()
    {
        //enemies = GameObject.FindObjectsOfType<Zombie>().ToList();

    }

    private void Update()
    {
        if (player != null)
        {
            health = player.GetComponent<health>().currentHealth;
        }
        else
        {
            health = 0;
        }
        WeaponText.text = weaponName;
        AmmoText.text = ammo + "/" + maxAmmo;
        GrenadeText.text = grenadeName;
        GrenadeCountText.text = grenadeCount.ToString();
        HealthText.text = "Health: " + health;
    }

}
