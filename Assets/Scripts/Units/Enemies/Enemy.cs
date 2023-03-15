using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float pv;
    protected float speed;
    protected float damage;

    public GameObject coinPrefab;

    public float get_damage()
    {
        return damage;
    }

    public void set_damage(float new_damage)
    {
        damage = new_damage;
    }

    private void die()
    {
        // Spawn the coin at the enemy's position
        Instantiate(coinPrefab, transform.position, Quaternion.identity);

        // Destroy the enemy
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Bullet bullet = hitInfo.GetComponent<Bullet>();
        if (bullet != null)
        {
            pv -= bullet.damage;
            if (pv <= 0)
            {
                die();
            }
        }
    }
}
