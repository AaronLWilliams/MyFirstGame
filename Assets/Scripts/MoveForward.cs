using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public int damage = 1;
    public float speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    void OnCollisionEnter2D(Collision2D Other)
    {   
        if (Other.gameObject.tag == "Player" || Other.gameObject.tag == "Enemy")
        {
            var healthcomponent = Other.gameObject.GetComponent<health>();
            healthcomponent.takeDamage(damage);
        }
        Destroy(this.gameObject);
    }

}
