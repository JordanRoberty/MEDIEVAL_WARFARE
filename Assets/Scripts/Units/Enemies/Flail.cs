using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flail : Enemy
{
    public Flail()
    {
        pv = 500000.0f;
        speed = 0.0f;
        damage = 50.0f;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Bullet bullet = hitInfo.GetComponent<Bullet>();
        if (bullet != null)
        {
            GameObject.Find("Boss").GetComponent<Boss>().pv -= bullet.damage;
        }
    }
}
