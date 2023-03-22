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

    private Boss boss;

    private void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Bullet bullet = hitInfo.GetComponent<Bullet>();
        if (bullet != null)
        {
            boss.pv -= bullet.damage;
            if (boss.pv <= 0)
            {
                Destroy(boss.gameObject);
            }
        }
    }
}
