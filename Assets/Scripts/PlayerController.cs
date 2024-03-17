using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] int maxHealth = 3;
    

    public Rigidbody2D rb;
    public GameManager gameManager;
    public GameObject player;

    Vector2 movementDirection;
    Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<health>().maxHealth = maxHealth;
        //UI Elements
        gameManager.health = player.GetComponent<health>().currentHealth;
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(player.GetComponent<health>().currentHealth <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection * moveSpeed;

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    
}
