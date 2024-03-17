using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zombie : MonoBehaviour
{
    //public static event Action<Zombie> OnZombieKilled;
    [SerializeField] int maxHealth = 3;
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb;
    Transform target;
    public GameObject zombie;
    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        zombie.GetComponent<health>().maxHealth = maxHealth;
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
        //If Zombie runs out of health
        if (zombie.GetComponent<health>().currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "Player" || Other.gameObject.tag == "Enemy")
        {
            var healthcomponent = Other.gameObject.GetComponent<health>();
            healthcomponent.takeDamage(1);
        }
    }
}
