using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Enemy
{
    public Bottle()
    {
        pv = 5000.0f;
        speed = 0.0f;
    }

    public GameObject puddle;
    private float life_time = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            other.gameObject.name == "Ground"
            || other.gameObject.name.Contains("Puddle")
            || other.gameObject.name == "Player"
        )
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        life_time -= Time.deltaTime;
        if (life_time < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(puddle, new Vector3(transform.position.x, -4f, 0f), Quaternion.identity);
    }
}
