using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public int maxHealth = 1;
    public int fuseTimer = 5;
    public int damage = 5;
    public float radius = 5f;
    public float force = 20f;
    public Rigidbody2D rb;
    public GameObject dynamite;
    // Start is called before the first frame update
    void Start()
    {
        dynamite.GetComponent<health>().maxHealth = maxHealth;
        StartCoroutine(ExplosionCountdown());
        //adds spin to the dynamite
        var impulse = (180 * Mathf.Deg2Rad) * rb.inertia;
        rb.AddTorque(impulse, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        noHealth();
    }

    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "Bullet")
        {
            explode();
        }
    }

    void explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var Object in colliders)
        {
            Rigidbody2D rb = Object.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                AddExplosionForce2D(rb.position, force, radius);//doesnt seem to work
            }
            var healthcomponent = Object.gameObject.GetComponent<health>();
            if (healthcomponent != null)
            {
                healthcomponent.takeDamage(damage);
            }
        }
        Destroy(this.gameObject);
    }

    void noHealth()
    {
        var healthcomponent = dynamite.GetComponent<health>();
        if (healthcomponent.currentHealth < 0)
        {
            explode();
        }
    }

    IEnumerator ExplosionCountdown()
    {
        yield return new WaitForSeconds(fuseTimer);
        explode();
    }

    void AddExplosionForce2D(Vector3 explosionOrigin, float explosionForce, float explosionRadius)
    {
        Vector3 direction = transform.position - explosionOrigin;
        float forceFalloff = 1 - (direction.magnitude / explosionRadius);
        GetComponent<Rigidbody2D>().AddForce(direction.normalized * (forceFalloff <= 0 ? 0 : explosionForce) * forceFalloff);
    }
}
