using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Enemy
{
    public Bottle()
    {
        pv = 5000.0f;
        speed = 0.0f;
        damage = 50.0f;
    }

    public GameObject puddle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            Instantiate(puddle, new Vector3(transform.position.x, -4f, 0f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
