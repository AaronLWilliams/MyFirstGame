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
    public TMP_Text AmmoText;
    public TMP_Text HealthText;
    public GameObject player;

    List<Zombie> enemies = new List<Zombie>();

    private void Awake()
    {
        //enemies = GameObject.FindObjectsOfType<Zombie>().ToList();

    }

    private void Update()
    {
        health = player.GetComponent<health>().currentHealth;
        AmmoText.text = ammo + "/" + maxAmmo;
        HealthText.text = "Health: " + health;
    }

}
