using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
        Ennemy enemy = hitInfo.GetComponent<Ennemy>();
        if (enemy != null)
        {
            enemy.take_damages(damage);
        }
    }
    
    void update()
    {
        if (Time.time > 2.0f)
        {
            Destroy(gameObject);
        }
    }
}
