using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : Enemy
{
    public Puddle()
    {
        pv = 5000.0f;
        speed = 0.0f;
        damage = 50.0f;
    }

    float life_time = 3f;

    private void Update()
    {
        life_time -= Time.deltaTime;
        if (life_time <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
