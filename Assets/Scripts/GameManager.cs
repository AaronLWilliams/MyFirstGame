using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public float health;
    public int ammo;
    public int maxAmmo;
    public TMP_Text AmmoText;
    public TMP_Text HealthText;
    List<Zombie> enemies = new List<Zombie>();

    private void Awake()
    {
        enemies = GameObject.FindObjectsOfType<Zombie>().ToList();

    }

    private void Update()
    {
        AmmoText.text = ammo + "/" + maxAmmo;
        HealthText.text = "Health: " + health;
    }

}
